using System.ComponentModel.DataAnnotations;

namespace DeveloperWepApi1.Model.RequestModels
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}