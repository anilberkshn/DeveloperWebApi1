using MongoDB.Driver;
using TaskWepApi1.Database.Interface;

namespace TaskWepApi1.Database.Context
{
    public class Context : IContext
    {
        private IMongoDatabase _mongoDatabase;

        public Context(IMongoClient mongoClient, string database)
        {
            _mongoDatabase = mongoClient.GetDatabase(database);
        }

        public IMongoCollection<T> DbMongoCollectionSet<T>(string collection)
        {
            return _mongoDatabase.GetCollection<T>(collection);
        }
    }
}