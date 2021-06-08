using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Dapr.Actors.Runtime;
using Microsoft.Extensions.Logging;
using Contracts;

namespace CsharpActor
{
    [Actor(TypeName = "CsharpActor")]
    class CsharpActor : Actor, IBotActor
    {    
        private const string StateName = "state";
        private readonly ILogger<CsharpActor> logger;

        private ActorState state = ActorState.NotInitialized;

        public CsharpActor(ActorHost actorHost, ILogger<CsharpActor> logger) : base(actorHost)
        {
            this.logger = logger;
        }

        protected override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            logger.LogInformation("[{0}] Activating", Id);

            var stateResult = await StateManager.TryGetStateAsync<ActorState>(StateName);
            if (stateResult.HasValue) state = stateResult.Value;
        }

        protected override async Task OnDeactivateAsync()
        {
            await base.OnDeactivateAsync();
            logger.LogInformation("[{0}] Deactivating", Id);            
        }

        public async Task InitializeAsync()
        {
            logger.LogInformation("[{0}] Initializing", Id);
            
            if (state != ActorState.NotInitialized) {
                throw new InvalidOperationException($"Actor is already been initialized. State = {state}");
            }
            state = ActorState.Ready;
            await SaveState();
        }

        public async Task StartFightAsync(string id)
        {        
            logger.LogInformation("[{0}] Starting fight", Id);
            if (state != ActorState.Ready) {
                throw new InvalidOperationException($"Actor cannot start fight as it's in invalid state. State = {state}");
            }
            state = ActorState.Fighting;
            await SaveState();
        }

        public Task<NextMoveResponse> NextMoveAsync(NextMoveRequest request)
        {
            if (state != ActorState.Fighting) {
                throw new InvalidOperationException($"Actor is in invalid state. State = {state}");
            }
            logger.LogInformation("[{0}] Executing move {1}", Id, request.MoveNumber);
            return Task.FromResult(new NextMoveResponse());
        }

        public async Task EndFightAsync()
        {        
            logger.LogInformation("[{0}] Ending fight", Id);
            state = ActorState.Ready;
            await SaveState();   
        }

        private async Task SaveState() {
            await StateManager.SetStateAsync(StateName, state);
            await StateManager.SaveStateAsync();
        }
    }
}