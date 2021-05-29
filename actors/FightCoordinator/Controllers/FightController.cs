using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contracts;
using Dapr;
using Dapr.Actors;
using Dapr.Actors.Client;

namespace FightCoordinator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        

        [HttpPost]
        public async Task Fight()
        {
            var actorId = new ActorId("actor1");
            var botActor = ActorProxy.Create<ICsharpActor>(actorId, "CsharpActor");
            await botActor.InitializeAsync();
            await botActor.StartFightAsync("Fight1");
            await botActor.NextMoveAsync(new NextMoveRequest());        
            await botActor.NextMoveAsync(new NextMoveRequest());        
            await botActor.NextMoveAsync(new NextMoveRequest());        
            await botActor.EndFightAsync();
        }
    }
}
