using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using IdentityServer1.Models.Authentication;
using IdentityServer1.MongoDb.Interface;
using MongoDB.Driver;

namespace IdentityServer1.MongoDb
{
    public class MongoRepository<T> : IMongoRepository<T> where T : User
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
       
        public async Task<T> FindOneAsync(Expression<Func<T, bool>> expression)
        { 
           var record = await _collection.Find(expression).FirstOrDefaultAsync();
           return record;
        } 

        public void Update(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition)
        {
            var filter = Builders<T>.Filter.Where(expression);  // builder a static helper class containing various builders
            var update = updateDefinition.Set(x => x.CreatedOn, DateTime.Now);
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

            var update = updateDefinition.Set(x => x.CreatedOn, DateTime.Now)
                .Set(x => x.EmailConfirmed, true);// hata almaması için geçici bool alan parametre
            _collection.FindOneAndUpdate<T>(filter, update);
        }
    }
}