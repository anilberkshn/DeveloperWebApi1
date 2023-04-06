using System;
using System.Net;
using System.Threading.Tasks;
using IdentityServerApi.IdentityRepository;
using IdentityServerApi.Model.Entities;
using IdentityServerApi.Model.RequestModels;

namespace IdentityServerApi.Services
{
    public class IdentityService : IIdentityService
    {
        private IIdentityRepository _identityRepository { get; set; }
        
        public IdentityService(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }
        

        public UserProperties GetById(Guid id)   // Todo : getByUsername, getByEmail araştırılacak.
        {
            var user = _identityRepository.GetById(id);

            if (user == null)
            {
                throw new NotImplementedException(); // todo: exceptionlar oluşturulacak.
            }

            if (user.IsDeleted)
            {
                throw new NotImplementedException();
            }

            return user;
        }

        public Task<Guid> InsertAsync(UserProperties user)
        {
            var addUser =  _identityRepository.InsertAsync(user);

            if (addUser == null)
            {
                throw new NotImplementedException(); 
            }
           
            return addUser ;
        }

        public void Update(Guid guid, UpdateUserDto updateUserDto)
        {
           _identityRepository.Update(guid,updateUserDto);
        }

        public Guid Delete(Guid guid)
        {
          return  _identityRepository.Delete(guid);
        }

        public void SoftDelete(Guid guid, SoftDeleteDto softDeleteDto)
        {
           _identityRepository.SoftDelete(guid,softDeleteDto);
        }
    }
}