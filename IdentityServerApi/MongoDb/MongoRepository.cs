using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityServerApi.Model.Entities;
using IdentityServerApi.Model.RequestModels;
using IdentityServerApi.MongoDb.Interface;
using MongoDB.Driver;

namespace IdentityServerApi.MongoDb
{
    public class MongoRepository<T> : IMongoRepository<T> where T : UserProperties
    {
        private readonly IMongoCollection<T> _collection;
        
        public MongoRepository(IContext context, string collectionName)
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
                
            await _collection.InsertOneAsync(record);
            return record.Id;
        }
        public async Task<IEnumerable<T>> FindAllAsync(GetAllDto getAllDto)
        {
            var record = _collection.AsQueryable().AsEnumerable();
            return record.Skip(getAllDto.skip).Take(getAllDto.take);
        }
        public async Task<T> FindOneAsync(Expression<Func<T, bool>> expression)
        { 
           var record = await _collection.Find(expression).FirstOrDefaultAsync();
           return record;
        } 

        public void Update(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition)
        {
            var filter = Builders<T>.Filter.Where(expression);  // builder a static helper class containing various builders
            var update = updateDefinition.Set(x => x.CreatedTime, DateTime.Now);
            _collection.FindOneAndUpdate<T>(filter, update); 
        }

        public Guid Delete(Expression<Func<T, bool>> expression)
        {
            var record = _collection.FindOneAndDelete(expression);
            return record.Id;
        }

        public void SoftDelete(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition)
        {
            var filter  = Builders<T>.Filter.Where(expression);

            var update = updateDefinition.Set(x => x.UpdatedTime, DateTime.Now)
                .Set(x => x.IsDeleted, true);
            _collection.FindOneAndUpdate<T>(filter, update);
        }
    }
}