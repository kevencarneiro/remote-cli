using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RemoteCLI.Application.Interfaces;
using RemoteCLI.Common.Interfaces;
using RemoteCLI.Common.Models;

namespace RemoteCLI.Api.Hubs
{
    public class RemoteCLIHub : Hub<IRemoteCLIClient>, IRemoteCLIServer
    {
        private readonly IClientAppService _clientAppService;
        private readonly IHubContext<RemoteCLIManagementHub> _managementContext;

        public RemoteCLIHub(IHubContext<RemoteCLIManagementHub> managementContext, IClientAppService clientAppService)
        {
            _managementContext = managementContext;
            _clientAppService = clientAppService;
        }

        public async Task CommandOutput(string output, MessageType messageType, string sender)
        {
            var client = _clientAppService.GetClientByConnectionId(Context.ConnectionId);
            await _managementContext.Clients.Client(sender).SendAsync("CommandOutput", output, messageType, client.Id.AsString);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.Caller.RequestMachineInfo();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var client = _clientAppService.GetClientByConnectionId(Context.ConnectionId);
            _clientAppService.UnregisterClient(Context.ConnectionId);
            await _managementContext.Clients.All.SendAsync("MachineUnregistered", client.Id.AsString);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task RequestMachineInfo(string client)
        {
            await Clients.Client(client).RequestMachineInfo();
        }

        public async Task SendCommand(string client, string sender, string command)
        {
            await Clients.Client(client).SendCommand(sender, command);
        }

        public async Task StoreMachineInfo(MachineInfo machineInfo)
        {
            _clientAppService.RegisterClient(machineInfo, Context.ConnectionId);
            await _managementContext.Clients.All.SendAsync("MachineRegistered", machineInfo);
        }
    }
}