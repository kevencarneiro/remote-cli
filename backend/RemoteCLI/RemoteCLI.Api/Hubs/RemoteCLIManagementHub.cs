using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RemoteCLI.Api.Interfaces;
using RemoteCLI.Application.Interfaces;
using RemoteCLI.Common.Models;

namespace RemoteCLI.Api.Hubs
{
    public class RemoteCLIManagementHub : Hub<IRemoteCLIManagementClient>, IRemoteCLIManagementServer
    {
        private readonly IClientAppService _clientAppService;
        private readonly IHubContext<RemoteCLIHub> _clientContext;

        public RemoteCLIManagementHub(IClientAppService clientAppService, IHubContext<RemoteCLIHub> clientContext)
        {
            _clientAppService = clientAppService;
            _clientContext = clientContext;
        }

        public async Task CommandOutput(string output, MessageType messageType, string machineId)
        {
            await Clients.All.CommandOutput(output);
        }

        public async Task MachineRegistered(MachineInfo machine)
        {
            await Clients.All.MachineRegistered(machine);
        }

        public async Task MachineUnregistered(string machineId)
        {
            await Clients.All.MachineUnregistered(machineId);
        }

        public override Task OnConnectedAsync()
        {
            _clientAppService.GetConnectedMachineInfo().ToList()
                .ForEach(x => Clients.Caller.MachineRegistered(x));
            return base.OnConnectedAsync();
        }

        public async Task SendCommand(string machineId, string command)
        {
            var client = _clientAppService.GetClientById(machineId);
            await _clientContext.Clients.Client(client.ConnenctionId).SendAsync("SendCommand", Context.ConnectionId, command);
        }
    }
}