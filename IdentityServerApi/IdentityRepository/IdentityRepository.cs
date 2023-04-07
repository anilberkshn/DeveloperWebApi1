using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServerApi.Model.Entities;
using IdentityServerApi.Model.RequestModels;
using IdentityServerApi.MongoDb;
using IdentityServerApi.MongoDb.Interface;
using MongoDB.Driver;

namespace IdentityServerApi.IdentityRepository
{
    public class IdentityRepository : MongoRepository<UserProperties>,IIdentityRepository
    {
        public IdentityRepository(IContext context, string collectionName = "Users") : base(context, collectionName)
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

        public async Task<IEnumerable<UserProperties>> GetAllAsync(GetAllDto getAllDto)
        {
            var userList = await FindAllAsync(getAllDto);
            return userList ;
        }
        public void Update(Guid userId, UpdateUserDto userUpdateDto)
        {
            var update = Builders<UserProperties>.Update
                    .Set(x => x.UserName, userUpdateDto.UserName)
                    .Set(x => x.Email, userUpdateDto.Email)
                    .Set(x => x.Password, userUpdateDto.Password)
                    .Set(x => x.UpdatedTime, userUpdateDto.UpdatedTime)
                    .Set(x => x.IsDeleted, userUpdateDto.IsDeleted)
                    .Set(x => x.DeleteTime, userUpdateDto.DeletedTime);

            Update(x=> x.Id == userId,update);
        }

        public Guid Delete(Guid guid)
        {
            return Delete(x => x.Id == guid);
        }

        public void SoftDelete(Guid guid, SoftDeleteDto softDeleteDto)
        {
            var softDelete = Builders<UserProperties>.Update
                .Set(x => x.DeleteTime, softDeleteDto.DeletedTime)
                .Set(x => x.IsDeleted , softDeleteDto.IsDeleted);

            SoftDelete(x => x.Id == guid,softDelete);
        }
    }
}