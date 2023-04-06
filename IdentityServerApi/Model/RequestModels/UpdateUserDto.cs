using System;

namespace IdentityServerApi.Model.RequestModels
{
    public class UpdateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime DeletedTime { get; set; }
        public bool IsDeleted{ get; set; }
    }
}