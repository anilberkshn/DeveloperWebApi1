using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;

namespace DeveloperWepApi1.Services
{
    public interface IDeveloperService
    {
        public Developer GetById(Guid id);
        public Task<IEnumerable<Developer>>GetAllAsync(int skip,int take);
        public Task<Guid> InsertDeveloperAsync(Developer developer);
        public void UpdateDeveloper(Guid developerId, UpdateDeveloperDto updateDeveloperDto);
        public Guid Delete(Guid guid);
        public void SoftDelete(Guid guid, SoftDeleteDto softDeleteDto);
    }
}