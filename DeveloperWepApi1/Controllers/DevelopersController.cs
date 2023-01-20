using System;
using System.Linq;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.ErrorModels;
using DeveloperWepApi1.Model.RequestModels;
using DeveloperWepApi1.Model.ResponseModels;
using DeveloperWepApi1.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperWepApi1.Controllers
{
    [ApiController]
    [Route("api/developer")]
    public class DevelopersController : ControllerBase
    {
        //public List<Developer> _developers { get; set; }
        private readonly IRepository _repository;

        public DevelopersController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult CreateDeveloper([FromBody] CreateDeveloperDto createDeveloperDto)
        {
            var developer = new Developer()
            {
                Id = Guid.NewGuid(),
                Name = createDeveloperDto.Name,
                Surname = createDeveloperDto.Surname,
                Department = createDeveloperDto.Department
            };
            _repository.InsertDeveloper(developer);

            var response = new DeveloperCreateResponse()
            {
                Id = developer.Id
            };
            return Ok(response);
        }

        [HttpPut]
        public IActionResult UpdateDeveloper(Guid developerId, [FromBody] UpdateDeveloperDto updateDeveloperDto)
        {
            var developer = _repository.GetById(developerId);
            _repository.UpdateDeveloper(developerId, updateDeveloperDto);
            return Ok(developer);
        }

        [HttpGet("{developerId}", Name = "developerId")]
        public IActionResult GetById(Guid developerId)
        {
            var findOne = _repository.GetById(developerId);
            return Ok(findOne);
        }

        [HttpGet("getAll")]  
        public IActionResult GetAll()
        {
            Console.WriteLine("getAll");
            var getAll = _repository.GetAll();
            return Ok(getAll);
        }

        [HttpDelete("{developerId}", Name = "developerId")]
        public IActionResult Delete(Guid id) // hard delete
        {
            var developer = _repository.GetById(id);
            _repository.Delete(developer.Id);
            return Ok(id);
        }

        [HttpPut("softDelete")]
        public IActionResult SoftDelete(Guid id, [FromBody] SoftDeleteDto softDeleteDto) // soft delete
        {
            var developer = _repository.GetById(id);
            _repository.SoftDelete(developer.Id, softDeleteDto);
            return Ok(id);
        }
        // soft delete 
    }
}


// softDelete Denemeler
// developer.Department = updateDeveloperDto.Department;
// developer.Name = updateDeveloperDto.Name;
// developer.Surname = updateDeveloperDto.Surname;
// developer.UpdatedTime = updateDeveloperDto.UpdatedTime;
/*-----SoftDeleteDto-----*/
// developer.DeleteTime = softDeleteDto.DeletedTime;
// developer.IsDeleted = softDeleteDto.IsDeleted;
//--------------------------

// eski developerUpdate
// developer.Department = updateDeveloperDto.Department;
// developer.Name = updateDeveloperDto.Name;
// developer.Surname = updateDeveloperDto.Surname;
// developer.UpdatedTime = updateDeveloperDto.UpdatedTime;