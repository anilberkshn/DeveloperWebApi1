using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;
using DeveloperWepApi1.DeveloperRepository;
using DeveloperWepApi1.Model.ErrorModels;

namespace DeveloperWepApi1.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IDeveloperRepository _developerRepository;

        public DeveloperService(IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }

        public Developer GetById(Guid id)
        {
            var developer = _developerRepository.GetById(id);
            
            if (developer == null)
            {
                throw new DeveloperException(HttpStatusCode.NotFound,"developer bulunamadı.");
            }
            if (developer.IsDeleted)
            {
                throw new DeveloperException(HttpStatusCode.NotFound, "developer bulunamadı.");
            }
            
            return developer;
        }

        public Task<IEnumerable<Developer>> GetAllAsync(string name)
        {
            return _developerRepository.GetAllAsync(name);
        }

        public Task<Guid> InsertDeveloperAsync(Developer developer)
        {
            return _developerRepository.InsertDeveloperAsync(developer);
        }

        public void UpdateDeveloper(Guid developerId, UpdateDeveloperDto updateDeveloperDto)
        {
           _developerRepository.UpdateDeveloper(developerId, updateDeveloperDto);
        }

        public Guid Delete(Guid developerId)
        {
            return _developerRepository.Delete(developerId);
        }

        public void SoftDelete(Guid developerId, SoftDeleteDto softDeleteDto)
        {
            _developerRepository.SoftDelete(developerId,softDeleteDto);  
        }
    }
}