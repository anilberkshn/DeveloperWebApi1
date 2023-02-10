// using Newtonsoft.Json;
// using System;  
// using System.Collections.Generic;  
// using System.Linq;  
// using System.Threading.Tasks;
//
//
// namespace DeveloperWepApi1.Model.Entities
// {
//     public class Users : IUserService
//     {
//         public int Id { get; set; }
//         public string FirstName { get; set; }
//         public string LastName { get; set; }
//        
//         public string Username { get; set; }
//
//         [JsonIgnore]
//         public string Password { get; set; }
//
//         public bool ValidateCredentials(string username, string password)
//         {
//             return username.Equals("admin") && password.Equals("admin");
//         }
//     }
// }