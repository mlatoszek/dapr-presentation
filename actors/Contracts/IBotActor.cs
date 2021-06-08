using System.Threading.Tasks;
using Dapr.Actors;

namespace Contracts
{
    public interface IBotActor : IActor
    {
        Task InitializeAsync();              
        Task StartFightAsync(string id);
        Task<NextMoveResponse> NextMoveAsync(NextMoveRequest request);        
        Task EndFightAsync();  
    }
}