using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Mongo.Interface;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace DeveloperWepApi1.Mongo
{
    public class MongoRepository<T> : IMongoRepository<T> where T : Document
    {
        private readonly IMongoCollection<T> _collection;
        
        public MongoRepository(IContext context, string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                collectionName = typeof(T).Name;
            }

            _collection = context.DbMongoCollectionSet<T>(collectionName);
            //Contexti kullanarak collectionumuzu almış olduk. 
        }
        public async Task<Guid> CreateAsync(T record)
        {
            record.Id = Guid.NewGuid();
            record.CreatedTime = DateTime.Now;
            record.UpdatedTime = DateTime.Now;
    
            await _collection.InsertOneAsync(record);
            return record.Id;
        }
        // public async Task<IEnumerable<T>> FindAllAsync()
        // {
        //    // var record = _collection.Find(document =>true ).Skip(5).Limit(5);
        //    // var record = await _collection.Find(expression).Skip(5).Limit(5).ToListAsync();
        //    var record = _collection.AsQueryable().AsEnumerable();
        //    return record;
        //} 
    
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T,bool>> expression)
        {
            var record2 = _collection.Find(expression)
                .Sort(expression.Name)
                .Skip(5).Limit(5)
                .ToEnumerable()
                ;
            
            var record4 = await _collection.Find(expression)
                .Sort(expression.Name)
                .Skip(5).Limit(5).ToListAsync();

            
            var record3 =  _collection.Find(expression)
                .Sort(expression.Name)
                .Skip(5).Limit(5).
                ToEnumerable().
                AsEnumerable().
                AsQueryable().
                ToList(); 
            //var record = _collection.Find( ).Sort(s).Limit(5);
           // var record = await _collection.Find(expression).Skip(5).Limit(5).ToListAsync();
           return record2;
        }
        
        public async Task<T> FindOneAsync(Expression<Func<T, bool>> expression)
        { 
           var record = await _collection.Find(expression).FirstOrDefaultAsync();
           return record;
        } 

        public void Update(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition)
        {
            var filter = Builders<T>.Filter.Where(expression);  // builder a static helper class containing various builders
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
            var filter  = Builders<T>.Filter.Where(expression);

            var update = updateDefinition.Set(x => x.DeleteTime, DateTime.Now)
                .Set(x => x.IsDeleted, true);
            _collection.FindOneAndUpdate<T>(filter, update);
        }
    }
}


// Update çalışmayan kodu
//      ÇALIŞMIYOR ALTTAKİ
// var update = Builders<T>.Update.Set(x=> x.UpdatedTime,DateTime.Now);
// _collection.UpdateOne(filter, update);
//---------------------------------------
// softdelete eski denemeler
//IQueryable<T>
//INotifyPropertyChanged
// var record = _collection.FindOneAndDelete(expression);
// record.DeleteTime = DateTime.Now;
// record.IsDeleted = true;
// return record.Id;

// var filter = Builders<T>.Filter.Where(expression);
// updateDefinition.Set(x => x.DeleteTime, DateTime.Now)
//     .Set(x =>x.IsDeleted,true);
// _collection.FindOneAndUpdate<T>(filter, updateDefinition);