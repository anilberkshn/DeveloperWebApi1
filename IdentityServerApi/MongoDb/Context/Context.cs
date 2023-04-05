using IdentityServerApi.MongoDb.Interface;
using MongoDB.Driver;

namespace IdentityServerApi.MongoDb.Context
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