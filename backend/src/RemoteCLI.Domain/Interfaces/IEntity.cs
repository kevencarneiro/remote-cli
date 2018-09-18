using LiteDB;

namespace RemoteCLI.Domain.Interfaces
{
    public interface IEntity
    {
        BsonValue Id { get; }
    }
}