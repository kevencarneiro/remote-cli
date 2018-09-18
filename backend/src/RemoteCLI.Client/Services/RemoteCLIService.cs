using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RemoteCLI.Client.Interfaces;

namespace RemoteCLI.Client.Services
{
    public class RemoteCLIService : IHostedService, IDisposable
    {
        private readonly ISignalRService _signalRService;

        public RemoteCLIService(ISignalRService signalRService)
        {
            _signalRService = signalRService;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task StartAsync(CancellationToken cancellationToken) => _signalRService.StartAsync(cancellationToken);

        public Task StopAsync(CancellationToken cancellationToken) => _signalRService.StopAsync(cancellationToken);
    }
}