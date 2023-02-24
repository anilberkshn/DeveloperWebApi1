using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.ErrorModels;
using DeveloperWepApi1.Model.RequestModels;
using DeveloperWepApi1.Mongo;
using DeveloperWepApi1.Mongo.Interface;
using MongoDB.Driver;

namespace DeveloperWepApi1.DeveloperRepository
{
    public class DeveloperRepository : MongoRepository<Developer>,IDeveloperRepository
    {
       
        public DeveloperRepository(IContext context, string collectionName = "Developers") : base(context, collectionName)
        {
           
        }

        public Developer GetById(Guid id)
        {
            var developer = FindOneAsync(x => x.Id == id).GetAwaiter().GetResult();
            return developer;
        }

        public async Task<IEnumerable<Developer>> GetAllAsync()
        { 
           return await FindAllAsync();
        }

        public async Task<Guid> InsertDeveloperAsync(Developer developer)
        {
          return await CreateAsync(developer);
        }
        public void UpdateDeveloper(Guid developerId, UpdateDeveloperDto updateDeveloperDto)
        {
            var update = Builders<Developer>.Update
                .Set(x => x.Name, updateDeveloperDto.Name)
                .Set(x => x.Surname, updateDeveloperDto.Surname)
                .Set(x => x.Department, updateDeveloperDto.Department)
                .Set(x => x.UpdatedTime, updateDeveloperDto.UpdatedTime)
                .Set(x => x.IsDeleted, updateDeveloperDto.IsDeleted)
                .Set(x => x.DeleteTime, updateDeveloperDto.DeletedTime)
                // .Set(x => x.RefreshToken, updateDeveloperDto.RefreshToken)
                // .Set(x => x.RefreshTokenEndDate, updateDeveloperDto.RefreshTokenEndDate)
                ;

            Update(x=> x.Id == developerId,update);

        }
        public Guid Delete(Guid guid)
        {
            return Delete(x => x.Id == guid);
        }
        public void SoftDelete(Guid guid,SoftDeleteDto softDeleteDto)
        {
            var softDelete = Builders<Developer>.Update
                .Set(x => x.DeleteTime, softDeleteDto.DeletedTime)
                .Set(x => x.IsDeleted , softDeleteDto.IsDeleted);

             SoftDelete(x => x.Id == guid,softDelete);
        }
    }
}