using System;
using System.Threading.Tasks;
using IdentityServer1.Models.Authentication;
using IdentityServer1.Models.RequestModels;
using IdentityServer1.MongoDb;
using IdentityServer1.MongoDb.Interface;

namespace IdentityServer1.IdentityRepository
{
    public class IdentityRepository : MongoRepository<User>,IIdentityRepository
    {
        public IdentityRepository(IContext context, string collectionName) : base(context, collectionName)
        {
        }

        public User GetById(Guid id)
        {
            var user = FindOneAsync(x => x.Id == id).GetAwaiter().GetResult();
            return user;
        }

        public async Task<Guid> InsertAsync(User user)
        {
            return await CreateAsync(user);
        }

        public void Update(Guid userId, User userUpdateDto)
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