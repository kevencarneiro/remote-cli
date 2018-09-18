using System.Collections.Generic;
using LiteDB;
using Microsoft.Extensions.Configuration;
using RemoteCLI.Domain.Interfaces;
using RemoteCLI.Domain.Models;

namespace RemoteCLI.Data.Repositories
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Client GetByConnectionId(string connectionId)
        {
            using (var db = new LiteRepository(ConnectionString))
            {
                return db.SingleOrDefault<Client>(x => x.ConnenctionId == connectionId);
            }
        }

        public IEnumerable<Client> GetConnectedClients()
        {
            using (var db = new LiteRepository(ConnectionString))
            {
                return db.Query<Client>().Where(x => x.ConnenctionId != null).ToList();
            }
        }
    }
}