using System.Collections.Generic;

namespace RemoteCLI.Domain.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        bool Delete(T item);

        T Get(string id);

        IEnumerable<T> GetAll();

        bool Insert(T item);

        bool Update(T item);

        bool Upsert(T item);
    }
}