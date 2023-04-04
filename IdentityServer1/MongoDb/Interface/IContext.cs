using MongoDB.Driver;

namespace IdentityServer1.MongoDb.Interface
{
    public interface IContext
    {
        public IMongoCollection<T> DbMongoCollectionSet<T>(string collection);
    }
}