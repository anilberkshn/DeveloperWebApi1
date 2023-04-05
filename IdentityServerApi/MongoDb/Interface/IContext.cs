using MongoDB.Driver;

namespace IdentityServerApi.MongoDb.Interface
{
    public interface IContext
    {
        public IMongoCollection<T> DbMongoCollectionSet<T>(string collection);
    }
}