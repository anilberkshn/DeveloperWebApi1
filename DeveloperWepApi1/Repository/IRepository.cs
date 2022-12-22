using System;
using System.Collections.Generic;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;

namespace DeveloperWepApi1.Repository
{
    public interface IRepository
    {
        public Developer GetById(Guid id);
        public List<Developer> GetAll();
        public Guid InsertDeveloper(Developer developer);
        public void UpdateDeveloper(Guid developerId, UpdateDeveloperDto updateDeveloperDto);
    }
}