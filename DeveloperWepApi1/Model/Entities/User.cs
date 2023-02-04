using MongoDB.Bson.Serialization.Attributes;

namespace DeveloperWepApi1.Model.Entities
{
    public class User
    {
        // public string Id { get; set; }
        
        [BsonElement("Username")]
        public string Username { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }
    }
}