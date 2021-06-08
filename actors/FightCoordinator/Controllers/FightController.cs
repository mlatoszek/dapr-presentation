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
using System.Threading;

namespace FightCoordinator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        

        [HttpPost]
        public async Task Fight()
        {
            var actor1Id = new ActorId("actor1");
            var actor2Id = new ActorId("actor2");
            var bot1Actor = ActorProxy.Create<IBotActor>(actor1Id, "CsharpActor");
            var bot2Actor = ActorProxy.Create<IBotActor>(actor2Id, "CsharpActor");

            await bot1Actor.InitializeAsync();
            await bot2Actor.InitializeAsync();

            Thread.Sleep(15 * 1000);

            await bot1Actor.StartFightAsync("Fight1");
            await bot2Actor.StartFightAsync("Fight1");


            await bot1Actor.NextMoveAsync(new NextMoveRequest(1));    
            await bot2Actor.NextMoveAsync(new NextMoveRequest(2));

            await bot1Actor.NextMoveAsync(new NextMoveRequest(3));                    
            await bot2Actor.NextMoveAsync(new NextMoveRequest(4));        

            

            await bot1Actor.EndFightAsync();
            await bot2Actor.EndFightAsync();
        }
    }
}
