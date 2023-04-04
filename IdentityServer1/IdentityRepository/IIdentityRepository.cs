using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer1.Models.Authentication;
using IdentityServer1.Models.RequestModels;

namespace IdentityServer1.IdentityRepository
{
    public interface IIdentityRepository
    {
        public User GetById(Guid id);
        
        public Task<Guid> InsertAsync(User user);
        
        public void Update(Guid userId, User userUpdateDto);
        
        public Guid Delete(Guid guid);
     
        public void SoftDelete(Guid guid, SoftDeleteDto softDeleteDto);
    }
    
}