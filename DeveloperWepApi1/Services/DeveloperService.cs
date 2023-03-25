using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using DeveloperWepApi1.Config;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;
using DeveloperWepApi1.DeveloperRepository;
using DeveloperWepApi1.Kafka;
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
                throw new DeveloperException(HttpStatusCode.NotFound, "developer bulunamadı.");
            }

            if (developer.IsDeleted)
            {
                throw new DeveloperException(HttpStatusCode.NotFound, "developer bulunamadı.");
            }

            return developer;
        }

        public Task<IEnumerable<Developer>> GetAllAsync(GetAllDto getAllDto)
        {
            if (getAllDto.skip < 0)
            {
                throw new ValidationErrorException(HttpStatusCode.BadRequest, "Skip cannot negative");
            }

            if (getAllDto.take is > 100 or < 0)
            {
                throw new ValidationErrorException(HttpStatusCode.TooManyRequests, "TooManyRequest");
            }

            return _developerRepository.GetAllAsync(getAllDto);// mock
        }

        public Task<Guid> InsertDeveloperAsync(Developer developer)
        {
            var addedDeveloper =  _developerRepository.InsertDeveloperAsync(developer);

            if (addedDeveloper == null)
            {
                throw new DeveloperException(HttpStatusCode.NotFound,"developer eklenemedi.");
            }
            var producer = new MyProducerBuilder();

            return addedDeveloper ;
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
            _developerRepository.SoftDelete(developerId, softDeleteDto);
        }
    }
}