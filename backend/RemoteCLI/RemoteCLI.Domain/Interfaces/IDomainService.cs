using System.Collections.Generic;
using LiteDB;

namespace RemoteCLI.Domain.Interfaces
{
    public interface IDomainService<T> where T : IEntity
    {
        bool DeleteItem(T item);

        IEnumerable<T> GetAllItems();

        T GetItem(BsonValue id);

        bool UpdateItem(T item);

        bool UpsertItem(T item);
    }
}