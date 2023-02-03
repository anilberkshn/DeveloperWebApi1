using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;

namespace DeveloperWepApi1.Repository
{
    public interface IRepository
    {
        public Developer GetById(Guid id);
        public Task<IEnumerable<Developer>>GetAll();
        public Task<Guid> InsertDeveloper(Developer developer);
        public void UpdateDeveloper(Guid developerId, UpdateDeveloperDto updateDeveloperDto);
        public Guid Delete(Guid guid);
        public void SoftDelete(Guid guid, SoftDeleteDto softDeleteDto);
        public string Authenticate(AuthenticateModel developer);

    }
}