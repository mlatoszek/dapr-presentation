using System.Threading.Tasks;
using Contracts;
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
                
        
        [HttpPost("fight-finished")]
        public Task FightFinished(FightParameters parameters)
        {
            logger.LogInformation("Invocation of fight finished with parameters: {Fight}", parameters);            
            return Task.CompletedTask;
        }
    }
}