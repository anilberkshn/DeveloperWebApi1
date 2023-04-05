using System;
using System.Threading.Tasks;
using IdentityServerApi.Model.Entities;
using IdentityServerApi.Model.RequestModels;
using IdentityServerApi.MongoDb;
using IdentityServerApi.MongoDb.Interface;

namespace IdentityServerApi.IdentityRepository
{
    public class IdentityRepository : MongoRepository<UserProperties>,IIdentityRepository
    {
        public IdentityRepository(IContext context, string collectionName) : base(context, collectionName)
        {
        }

        public UserProperties GetById(Guid id)
        {
            var user = FindOneAsync(x => x.Id == id).GetAwaiter().GetResult();
            return user;
        }

        public async Task<Guid> InsertAsync(UserProperties user)
        {
            return await CreateAsync(user);
        }

        public void Update(Guid userId, UserProperties userUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Guid Delete(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(Guid guid, SoftDeleteDto softDeleteDto)
        {
            throw new NotImplementedException();
        }
    }
}