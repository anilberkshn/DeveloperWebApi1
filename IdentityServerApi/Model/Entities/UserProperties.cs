using System.Net.Mail;
using MongoDB.Driver;

namespace IdentityServerApi.Model.Entities
{
    public class UserProperties : Document  // : MongoIdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        
        // public MailAddress Email { get; set; }
        // public PasswordEvidence PasswordEvidence { get; set; }
    }
}