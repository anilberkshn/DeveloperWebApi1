using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace TaskWepApi1.Database.Interface
{
    public interface IGenericRepository<T>
    {
        public Task<Guid> CreateAsync (T record);
        public Task<IEnumerable<T>> FindAllAsync();
        // public Task<List<T>> FindAllAsync();
        public  Task<T> FindOneAsync(Expression<Func<T, bool>> expression);
        public void Update(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition);
        public Guid Delete(Expression<Func<T, bool>> expression);
        public void SoftDelete(Expression<Func<T, bool>> expression,UpdateDefinition<T> updateDefinition);
    }
}