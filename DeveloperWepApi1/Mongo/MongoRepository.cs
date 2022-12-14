using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;
using DeveloperWepApi1.Mongo.Interface;
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

        public Guid Create(T record)
        {
            record.Id = Guid.NewGuid();
            record.CreatedTime = DateTime.Now;
            record.UpdatedTime = DateTime.Now;

            _collection.InsertOne(record);
            //
            return record.Id;
        }

        public List<T> FindAll()
        {
            var record = _collection.AsQueryable().ToList();
            return record;
        }

        public T FindOne(Expression<Func<T, bool>> expression)
        {
            var records = _collection.Find(expression);
            var record = records.FirstOrDefault();
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