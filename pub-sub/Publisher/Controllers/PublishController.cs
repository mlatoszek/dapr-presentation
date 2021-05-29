using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Publisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : ControllerBase
    {        
        private readonly ILogger<PublishController> _logger;

        public PublishController(ILogger<PublishController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task ExecuteFight([FromServices] DaprClient client, [FromBody]FightParameters parameters)
        {
            _logger.LogInformation("Publishing fight {Fight}", parameters);
            await client.PublishEventAsync("pubsub", "fight", parameters);
            _logger.LogInformation("Published fight {Fight}", parameters);
        }
    }
}
