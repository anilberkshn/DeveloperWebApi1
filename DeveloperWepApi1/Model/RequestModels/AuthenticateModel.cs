using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace DeveloperWepApi1.Model.RequestModels
{
    public class AuthenticateModel
    {
        [Required]
        //[BsonElement("Username")]
        public string Username { get; set; }

        [Required]
        // [BsonElement("Password")]
        public string Password { get; set; }
    }
}