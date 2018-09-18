using System.Threading;
using System.Threading.Tasks;

namespace RemoteCLI.Client.Interfaces
{
    public interface ISignalRService
    {
        Task StartAsync(CancellationToken cancellationToken = default);

        Task StopAsync(CancellationToken cancellationToken = default);
    }
}