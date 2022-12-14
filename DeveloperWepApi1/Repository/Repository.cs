using System;
using System.Collections.Generic;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;
using DeveloperWepApi1.Mongo;
using DeveloperWepApi1.Mongo.Interface;
using MongoDB.Driver;

namespace DeveloperWepApi1.Repository
{
    public class Repository : MongoRepository<Developer>,IRepository
    {
        public Repository(IContext context, string collectionName = "Developers") : base(context, collectionName)
        {
        }

        public Developer GetById(Guid id)
        {
            return FindOne(x => x.Id == id);
        }

        public List<Developer> GetAll()
        {
            return FindAll();
        }

        public Guid InsertDeveloper(Developer developer)
        {
            return Create(developer);
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
                ;

            Update(x=> x.Id == developerId,update);

        }

        public Guid Delete(Guid guid)
        {
            return Delete(x => x.Id == guid);
        }
        
        public void SoftDelete(Guid guid,SoftDeleteDto softDeleteDto)
        {
            var softdelete = Builders<Developer>.Update
                .Set(x => x.DeleteTime, softDeleteDto.DeletedTime)
                .Set(x => x.IsDeleted , softDeleteDto.IsDeleted);

             SoftDelete(x => x.Id == guid,softdelete);
        }
    }
}