using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using TaskWepApi1.Database.Interface;
using TaskWepApi1.Model.Entities;

namespace TaskWepApi1.Database
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Document
    {
        private readonly IMongoCollection<T> _collection;

        public GenericRepository(IContext context,string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                collectionName = typeof(T).Name;
            }
            _collection = context.DbMongoCollectionSet<T>(collectionName);
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
            var record = _collection.AsQueryable().ToList(); // asQueryable  creates a queryable source of documents
            // ofset limit skkip ve take
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
            var filter = Builders<T>.Filter.Where(expression);

            var update = updateDefinition.Set(x => x.DeleteTime, DateTime.Now)
                .Set(x => x.IsDeleted, true);
            _collection.FindOneAndUpdate<T>(filter, update);
        }
    }
}