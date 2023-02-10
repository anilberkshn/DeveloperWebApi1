using MongoDB.Bson.Serialization.Attributes;

namespace DeveloperWepApi1.Model.Entities
{
    public class User
    {
        // public string Id { get; set; }
        
        
        public string Username { get; set; }

    
        public string Password { get; set; }
    }
}