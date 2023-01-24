using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeveloperWepApi1.Model.RequestModels
{
    public class CreateDeveloperDto 
    {
        // [Required]
        // [StringLength(15,ErrorMessage = "Ad 15 harfi gecemez")]
        public string Name { get; set; }
        // [Required]
        // [StringLength(10,ErrorMessage = "Soyad 10 harfi gecemez")]
        public string Surname { get; set; }
        // [Required]
        public string Department { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
    }
}