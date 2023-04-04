using System;

namespace IdentityServer1.Models.RequestModels
{
    public class SoftDeleteDto
    {
        public DateTime DeletedTime { get; set; }
        public bool IsDeleted{ get; set; }
    }
}