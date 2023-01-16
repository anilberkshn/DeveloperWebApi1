using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace TaskWepApi1.Database.Interface
{
    public interface IGenericRepository<T>
    {
        public Guid Create (T record);
        public List<T> FindAll();
        public T FindOne(Expression<Func<T, bool>> expression);
        public void Update(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition);
        public Guid Delete(Expression<Func<T, bool>> expression);
        public void SoftDelete(Expression<Func<T, bool>> expression,UpdateDefinition<T> updateDefinition);
        
    }
}