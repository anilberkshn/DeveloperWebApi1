using IdentityServer1.MongoDb.Interface;
using MongoDB.Driver;

namespace IdentityServer1.MongoDb.Context
{
    public class Context : IContext
    {
        private readonly IMongoDatabase _mongoDatabase;

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