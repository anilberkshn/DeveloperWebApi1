using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace IdentityServerApi.MongoDb.Interface
{
    public interface IMongoRepository<T>
    {
        
        public Task<Guid> CreateAsync (T record);
        // public Task<IEnumerable<T>> FindAllAsync(GetAllDto getAllDto);
        // public Task<List<T>> FindAllAsync();
        public  Task<T> FindOneAsync(Expression<Func<T, bool>> expression);
        public void Update(Expression<Func<T, bool>> expression, UpdateDefinition<T> updateDefinition);
        public Guid Delete(Expression<Func<T, bool>> expression);
        public void SoftDelete(Expression<Func<T, bool>> expression,UpdateDefinition<T> updateDefinition);

        
    }
}