using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RemoteCLI.Client.Interfaces;
using RemoteCLI.Client.Services;

namespace RemoteCLI.Client
{
    internal class RemoteCLIAgent
    {
        private readonly ISignalRService _signalRService;

        public RemoteCLIAgent()
        {
            var serviceProvider = RegisterDependencies();
            _signalRService = serviceProvider.GetService<ISignalRService>();
        }

        public Task Start() => _signalRService.Start();

        public Task Stop() => _signalRService.Stop();

        private static ServiceProvider RegisterDependencies()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IMachineService, MachineService>()
                .AddSingleton<IPowerShellService, PowerShellService>()
                .AddSingleton<ISignalRService, SignalRService>()
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}