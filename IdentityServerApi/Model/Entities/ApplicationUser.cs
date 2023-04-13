using Microsoft.AspNetCore.Identity;

namespace IdentityServerApi.Model.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}