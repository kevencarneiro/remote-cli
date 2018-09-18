using System.Collections.Generic;
using AutoMapper;
using RemoteCLI.Application.Interfaces;
using RemoteCLI.Common.Models;
using RemoteCLI.Domain.Interfaces;
using RemoteCLI.Domain.Models;

namespace RemoteCLI.Application.AppServices
{
    public class ClientAppService : IClientAppService
    {
        private readonly IClientDomainService _domainService;

        public ClientAppService(IClientDomainService domainService)
        {
            _domainService = domainService;
        }

        public Client GetClientByConnectionId(string connectionId)
            => _domainService.GetItemByConnectionId(connectionId);

        public Client GetClientById(string id)
            => _domainService.GetItem(id);

        public IEnumerable<Client> GetConnectedClients() => _domainService.GetConnectedClients();

        public IEnumerable<MachineInfo> GetConnectedMachineInfo() => Mapper.Map<IEnumerable<MachineInfo>>(GetConnectedClients());

        public void RegisterClient(MachineInfo machineInfo, string connectionId)
        {
            var client = Mapper.Map<Client>(machineInfo);
            client.ConnenctionId = connectionId;
            _domainService.UpsertItem(client);
        }

        public void UnregisterClient(string connectionId)
        {
            var item = _domainService.GetItemByConnectionId(connectionId);

            if (item == null) return;

            item.ConnenctionId = null;
            _domainService.UpdateItem(item);
        }
    }
}