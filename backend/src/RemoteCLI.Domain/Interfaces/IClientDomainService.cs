using System.Collections.Generic;
using RemoteCLI.Domain.Models;

namespace RemoteCLI.Domain.Interfaces
{
    public interface IClientDomainService : IDomainService<Client>
    {
        IEnumerable<Client> GetConnectedClients();

        Client GetItemByConnectionId(string connectionId);
    }
}