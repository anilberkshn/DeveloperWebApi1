using System;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;
using DeveloperWepApi1.Model.ResponseModels;
using DeveloperWepApi1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperWepApi1.Controllers
{
    [Authorize] // denemeler sırasında her seferinde giriş yapmamak için.
    [ApiController]
    [Route("api/developer")]
    public class DevelopersController : ControllerBase
    {
        private readonly IDeveloperService _developerService;
        public DevelopersController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }
        
        [HttpPost]
        public IActionResult CreateDeveloper([FromBody] CreateDeveloperDto createDeveloperDto)
        {
            var developer = new Developer()
            {
                Id = Guid.NewGuid(),
                Name = createDeveloperDto.Name,
                Surname = createDeveloperDto.Surname,
                Department = createDeveloperDto.Department,
            };
            _developerService.InsertDeveloperAsync(developer);

            var response = new DeveloperCreateResponse()
            {
                Id = developer.Id
            };
            return Ok(response);
        }
       
        [HttpPut]
        public IActionResult UpdateDeveloper(Guid developerId, [FromBody] UpdateDeveloperDto updateDeveloperDto)
        {
            var developer = _developerService.GetById(developerId);
            _developerService.UpdateDeveloper(developerId, updateDeveloperDto);
            return Ok(developer);
        }
        
        [HttpGet("{developerId}", Name = "developerId")]
        public IActionResult GetById(Guid developerId)
        {
            var findOne = _developerService.GetById(developerId);
            return Ok(findOne);
        }

        [HttpGet("GetAllDeveloper")]  
        public IActionResult GetAll([FromQuery]GetAllDto getAllDto)
        {
            var getAllDeveloper = _developerService.GetAllAsync(getAllDto);
            return Ok(getAllDeveloper);
        }
        
        [HttpDelete("{developerId}", Name = "developerId")]
        public IActionResult Delete(Guid id) // hard delete
        {
            var developer = _developerService.GetById(id);
            _developerService.Delete(developer.Id);
            return Ok(id);
        }
       
        [HttpPut("softDelete")]
        public IActionResult SoftDelete(Guid id, [FromBody] SoftDeleteDto softDeleteDto) // soft delete
        {
            var developer = _developerService.GetById(id);
            _developerService.SoftDelete(developer.Id, softDeleteDto);
            return Ok(id);
        }
    }
}

