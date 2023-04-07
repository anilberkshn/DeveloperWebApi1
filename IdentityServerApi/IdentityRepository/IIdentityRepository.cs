using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServerApi.Model.Entities;
using IdentityServerApi.Model.RequestModels;

namespace IdentityServerApi.IdentityRepository
{
    public interface IIdentityRepository
    {
        public UserProperties GetById(Guid id);
        
        public Task<Guid> InsertAsync(UserProperties user);
        
        public Task<IEnumerable<UserProperties>>GetAllAsync(GetAllDto getAllDto);
        
        public void Update(Guid userId, UpdateUserDto userUpdateDto);
        
        public Guid Delete(Guid guid);
     
        public void SoftDelete(Guid guid, SoftDeleteDto softDeleteDto);
        
    }
    
}