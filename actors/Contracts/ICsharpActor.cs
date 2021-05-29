using System.Threading.Tasks;
using Dapr.Actors;

namespace Contracts
{
    public interface ICsharpActor : IActor
    {
        Task InitializeAsync();              
        Task StartFightAsync(string id);
        Task<NextMoveResponse> NextMoveAsync(NextMoveRequest request);        
        Task EndFightAsync();  
    }
}