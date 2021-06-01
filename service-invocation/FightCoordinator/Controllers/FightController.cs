using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contracts;
using Dapr.Client;
using System.Net.Http.Json;

namespace FightCoordinator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        private readonly ILogger<FightController> logger;

        public FightController(ILogger<FightController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public async Task Fight(FightParameters parameters)
        {            
            var client = DaprClient.CreateInvokeHttpClient(appId: "web");

            logger.LogInformation("Executing of fight finished with parameters: {Fight}", parameters);            
            var response = await client.PostAsJsonAsync("/subscribe/fight-finished", parameters);
            logger.LogInformation("Execution finished");            
        }
    }
}
