using Newtonsoft.Json;

namespace DeveloperWepApi1.Model.Entities
{
    public class Users
    {
        public bool ValidateCredentials(string username, string password)  
        {  
            return username.Equals("admin") && password.Equals("admin");  
        }  
    }
}