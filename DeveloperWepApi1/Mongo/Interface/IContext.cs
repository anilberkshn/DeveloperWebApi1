using MongoDB.Driver;

namespace DeveloperWepApi1.Mongo.Interface
{
    public interface IContext
    {
        public IMongoCollection<T> DbMongoCollectionSet<T>(string collection);
    }
}