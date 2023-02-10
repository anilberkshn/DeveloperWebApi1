using System;
using System.ComponentModel.DataAnnotations;

namespace DeveloperWepApi1.Model.RequestModels
{
    public class UpdateDeveloperDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Department { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime DeletedTime { get; set; }
        public bool IsDeleted{ get; set; }
        
        public string Username { get; set; }
        public string Password { get; set; }
        
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
    }
}

// validation denemeeleri
// [Required]
// [StringLength(15,ErrorMessage = "Ad 15 harfi gecemez")]

// [Required]
// [StringLength(10,ErrorMessage = "Soyad 10 harfi gecemez")]

// [Required]