using System.Collections.Generic;
using LiteDB;
using RemoteCLI.Domain.Interfaces;

namespace RemoteCLI.Domain.DomainServices
{
    public abstract class DomainServiceBase<T, TRepository> : IDomainService<T> where T : IEntity where TRepository : IRepository<T>
    {
        protected readonly TRepository Repository;

        internal DomainServiceBase(TRepository repository)
        {
            Repository = repository;
        }

        public bool DeleteItem(T item) => Repository.Delete(item);

        public IEnumerable<T> GetAllItems() => Repository.GetAll();

        public T GetItem(BsonValue id) => Repository.Get(id);

        public bool UpdateItem(T item) => Repository.Update(item);

        public bool UpsertItem(T item) => Repository.Upsert(item);
    }
}