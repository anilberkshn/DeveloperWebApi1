using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;
using MongoDB.Driver;

namespace DeveloperWepApi1.Mongo.Interface
{
    public interface IMongoRepository<T>
    {
        public Guid Create (T record);
        public List<T> FindAll();
        public T FindOne(Expression<Func<T, bool>> expression);

        public void Update(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition);
    
    }
}