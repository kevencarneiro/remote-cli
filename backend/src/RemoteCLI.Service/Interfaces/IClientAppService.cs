using System.Collections.Generic;
using RemoteCLI.Common.Models;
using RemoteCLI.Domain.Models;

namespace RemoteCLI.Application.Interfaces
{
    public interface IClientAppService
    {
        Client GetClientByConnectionId(string connectionId);

        Client GetClientById(string id);

        IEnumerable<Client> GetConnectedClients();

        IEnumerable<MachineInfo> GetConnectedMachineInfo();

        void RegisterClient(MachineInfo machineInfo, string connectionId);

        void UnregisterClient(string connectionId);
    }
}