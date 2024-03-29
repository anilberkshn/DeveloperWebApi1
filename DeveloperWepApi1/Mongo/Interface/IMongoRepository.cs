using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;
using MongoDB.Driver;

namespace DeveloperWepApi1.Mongo.Interface
{
    public interface IMongoRepository<T>
    {
        public Task<Guid> CreateAsync (T record);
        public Task<IEnumerable<T>> FindAllAsync(GetAllDto getAllDto);
        // public Task<List<T>> FindAllAsync();
        public  Task<T> FindOneAsync(Expression<Func<T, bool>> expression);
        public void Update(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition);
        public Guid Delete(Expression<Func<T, bool>> expression);
        public void SoftDelete(Expression<Func<T, bool>> expression,UpdateDefinition<T> updateDefinition);
    }
}