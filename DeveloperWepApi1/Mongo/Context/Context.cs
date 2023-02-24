using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Mongo.Interface;
using MongoDB.Driver;

namespace DeveloperWepApi1.Mongo.Context
{
    public class Context : IContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        //Bize bir mongo client geliyor, gelen mongoclienti ile database ile alıyoruz
        //hangi databasede çalışacaksa..
        public Context(IMongoClient mongoClient, string database)
        {
            _mongoDatabase = mongoClient.GetDatabase(database);
        }

        public IMongoCollection<T> DbMongoCollectionSet<T>(string collection)
        {
            return _mongoDatabase.GetCollection<T>(collection);
        }
        // Collectionumuzu da repositoryinin içine ekledik.
    }
}