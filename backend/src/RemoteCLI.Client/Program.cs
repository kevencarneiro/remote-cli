using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RemoteCLI.Client.Extensions;
using RemoteCLI.Client.Interfaces;
using RemoteCLI.Client.Services;

namespace RemoteCLI.Client
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var isService = true;// !(Debugger.IsAttached || args.Contains("--console"));

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IMachineService, MachineService>();
                    services.AddSingleton<IPowerShellService, PowerShellService>();
                    services.AddSingleton<ISignalRService, SignalRService>();
                    services.AddHostedService<RemoteCLIService>();
                });

            if (isService)
            {
                await builder.RunAsServiceAsync();
            }
            else
            {
                await builder.RunConsoleAsync();
            }
        }
    }
}