using LiteDB;
using RemoteCLI.Common.Models;
using RemoteCLI.Domain.Interfaces;

namespace RemoteCLI.Domain.Models
{
    public class Client : MachineInfo, IEntity
    {
        public string ConnenctionId { get; set; }
        public new BsonValue Id => base.Id;
    }
}