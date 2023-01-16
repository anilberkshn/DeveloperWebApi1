using MongoDB.Driver;

namespace TaskWepApi1.Database.Interface
{
    public interface IContext
    {
        public IMongoCollection<T> DbMongoCollectionSet<T>(string collection);
    }
}