using System.Collections.Generic;
using RemoteCLI.Domain.Interfaces;
using RemoteCLI.Domain.Models;

namespace RemoteCLI.Domain.DomainServices
{
    public class ClientDomainService : DomainServiceBase<Client, IClientRepository>, IClientDomainService
    {
        public ClientDomainService(IClientRepository repository) : base(repository)
        {
        }

        public IEnumerable<Client> GetConnectedClients() => Repository.GetConnectedClients();

        public Client GetItemByConnectionId(string connectionId) => Repository.GetByConnectionId(connectionId);
    }
}