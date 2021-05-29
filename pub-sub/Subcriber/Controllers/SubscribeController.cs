using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Dapr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Subcriber.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscribeController : ControllerBase
    {
        private readonly ILogger<SubscribeController> logger;

        public SubscribeController(ILogger<SubscribeController> logger)
        {
            this.logger = logger;
        }
                

        [Topic("pubsub", "fight")]
        [HttpPost("fight")]
        public Task Fight(FightParameters parameters)
        {
            logger.LogInformation("Executing fight {Fight}", parameters);            
            return Task.CompletedTask;
        }
    }
}