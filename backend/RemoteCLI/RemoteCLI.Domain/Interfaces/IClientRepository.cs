using System.Collections.Generic;
using RemoteCLI.Domain.Models;

namespace RemoteCLI.Domain.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Client GetByConnectionId(string connectionId);

        IEnumerable<Client> GetConnectedClients();
    }
}