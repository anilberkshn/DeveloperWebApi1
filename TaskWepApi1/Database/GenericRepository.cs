using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using TaskWepApi1.Database.Interface;
using TaskWepApi1.Model.Entities;

namespace TaskWepApi1.Database
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Document
    {
        private readonly IMongoCollection<T> _collection;

        public GenericRepository(IContext context, string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                collectionName = typeof(T).Name;
            }

            _collection = context.DbMongoCollectionSet<T>(collectionName);
        }

        public async Task<Guid> CreateAsync(T record)
        {
            record.Id = Guid.NewGuid();
            record.CreatedTime = DateTime.Now;
            record.UpdatedTime = DateTime.Now;

            await _collection.InsertOneAsync(record);
            return record.Id;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            var record = _collection.AsQueryable().AsEnumerable();
            return record;
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> expression)
        {
            var record = await _collection.Find(expression).FirstOrDefaultAsync();
            return record;
        }

        public void Update(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition)
        {
            var filter = Builders<T>.Filter.Where(expression);
            var update = updateDefinition.Set(x => x.UpdatedTime, DateTime.Now);
            _collection.FindOneAndUpdate<T>(filter, update);
        }

        public Guid Delete(Expression<Func<T, bool>> expression)
        {
            var record = _collection.FindOneAndDelete(expression);
            return record.Id;
        }

        public void SoftDelete(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition)
        {
            var filter = Builders<T>.Filter.Where(expression);

            var update = updateDefinition.Set(x => x.DeleteTime, DateTime.Now)
                .Set(x => x.IsDeleted, true);
            _collection.FindOneAndUpdate<T>(filter, update);
        }
    }
}