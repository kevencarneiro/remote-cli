using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using RemoteCLI.Client.Interfaces;
using RemoteCLI.Common.Interfaces;
using RemoteCLI.Common.Models;
using static RemoteCLI.Common.Wrappers.Resiliency;

namespace RemoteCLI.Client.Services
{
    public class SignalRService : ISignalRService
    {
        private readonly HubConnection _connection;
        private readonly IMachineService _machineService;
        private readonly IPowerShellService _powerShellService;
        private bool _isStarted = false;

        public SignalRService(IMachineService machineService, IPowerShellService powerShellService)
        {
            _machineService = machineService;
            _powerShellService = powerShellService;

            _connection = BuildConnection();
            RegisterEvents(_connection);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _isStarted = true;
            return ExecuteOrRetryAsync(() => _connection.StartAsync(cancellationToken));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _isStarted = false;
            return ExecuteOrRetryAsync(() => _connection.StopAsync(cancellationToken));
        }

        private HubConnection BuildConnection()
            => new HubConnectionBuilder()
                .WithUrl("https://localhost:44330/RemoteCLIHub")
                .Build();

        private void RegisterEvents(HubConnection connection)
        {
            connection.On(nameof(IRemoteCLIClient.RequestMachineInfo),
                async () =>
                {
                    await ExecuteOrRetryAsync(() => connection.SendAsync(
                            nameof(IRemoteCLIServer.StoreMachineInfo),
                            _machineService.GetMachineInfo()
                        )
                    );
                });

            connection.On(nameof(IRemoteCLIClient.SendCommand),
                (string sender, string command) =>
                {
                    Task OnCommandOutput(string data, MessageType messageType, string returnTo) => ExecuteOrRetryAsync(
                        () => connection.SendAsync(nameof(IRemoteCLIServer.CommandOutput), data, messageType, returnTo)
                    );

                    _powerShellService.ExecuteCommand(sender, command, OnCommandOutput);
                });

            connection.Closed += (e)
                => _isStarted ? ExecuteOrRetryAsync(() => StartAsync(CancellationToken.None)) : Task.CompletedTask;
        }
    }
}