using System;

namespace IdentityServerApi.Model.RequestModels
{
    public class SoftDeleteDto
    {
        public DateTime DeletedTime { get; set; }
        public bool IsDeleted{ get; set; }
    }
}