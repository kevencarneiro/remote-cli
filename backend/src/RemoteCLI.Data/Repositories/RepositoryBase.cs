using System.Collections.Generic;
using LiteDB;
using Microsoft.Extensions.Configuration;
using RemoteCLI.Domain.Interfaces;

namespace RemoteCLI.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : IEntity
    {
        protected readonly string ConnectionString;

        protected RepositoryBase(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("RemoteCLIDatabase");
        }

        public bool Delete(T item)
        {
            using (var db = new LiteRepository(ConnectionString))
            {
                return db.Delete<T>(item.Id);
            }
        }

        public T Get(string id)
        {
            using (var db = new LiteRepository(ConnectionString))
            {
                return db.Query<T>().SingleById(id);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var db = new LiteRepository(ConnectionString))
            {
                return db.Query<T>().ToEnumerable();
            }
        }

        public bool Insert(T item)
        {
            using (var db = new LiteRepository(ConnectionString))
            {
                return db.Insert(item);
            }
        }

        public bool Update(T item)
        {
            using (var db = new LiteRepository(ConnectionString))
            {
                return db.Update(item);
            }
        }

        public bool Upsert(T item)
        {
            using (var db = new LiteRepository(ConnectionString))
            {
                return db.Upsert(item);
            }
        }
    }
}