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
                .Set(x => x.UpdatedTime, updateDeveloperDto.UpdatedTime);
            
            Update(x=> x.Id == developerId,update);

        }

        public Guid Delete(Guid guid)
        {
            return Delete(x => x.Id == guid);
        }
    }
}