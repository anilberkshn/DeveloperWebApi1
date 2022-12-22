using System;
using System.Collections.Generic;
using System.Linq;
using DeveloperWepApi1.Model;
using DeveloperWepApi1.Model.Entities;
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

        //------------------------------------------------------------------------
        [HttpPost("CreateDeveloper")]
        public IActionResult CreateDeveloper([FromBody] CreateDeveloperDto createDeveloperDto)
        {
            var developer = new Developer()
            {
                Id = Guid.NewGuid(),
                Name = createDeveloperDto.Name,
                Surname = createDeveloperDto.Surname
            };
            _repository.InsertDeveloper(developer);
 
            var response = new DeveloperCreateResponse()
            {
                Id = developer.Id
            };
            return Ok(response);
        }
        // [HttpPost("AddDeveloper")]
        // public IActionResult AddDeveloper(Developer developer)
        // {
        //     _developers.Add(developer);
        //     return Ok(developer.Id);
        // }

        // [HttpGet("SearchDeveloper")]
        // public IActionResult SearchDeveloper([FromQuery] SearchDeveloperDto searchDeveloperDto)
        // // bulamadı
        // {
        //     var developers = _developers.Where(x =>
        //         x.Name.Contains(searchDeveloperDto.Name, StringComparison.OrdinalIgnoreCase));
        //     if (developers.Any())
        //     {
        //         return NotFound();
        //     }
        //
        //     return Ok(developers);
        // }

        [HttpPut("developerId:guid")]
        public IActionResult UpdateDeveloper(Guid developerId, [FromBody] UpdateDeveloperDto updateDeveloperDto)
        {
            var developer = _repository.GetById(developerId);
            if (developer == null)
            {
                return NotFound();
            }

            developer.Department = updateDeveloperDto.Department;
            developer.Name = updateDeveloperDto.Name;
            developer.Surname = updateDeveloperDto.Surname;
            developer.UpdatedTime = updateDeveloperDto.UpdatedTime;
            return Ok();
        }
        //--------------------------------------------------------------------------------------

        [HttpGet("{developerId}", Name = "developerId")]
        public IActionResult GetById(Guid developerId)
        {
            var findOne = _repository.GetById(developerId);
            return Ok(findOne);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var getAll = _repository.GetAll();
            return Ok(getAll);
        }

       // [HttpDelete("delete")]
        // public IActionResult Delete(Guid id)
        // {
        //     //  var developer  = _developers.Remove(_developers.FirstOrDefault(x => x.Id == id));
        //     var developer = _developers.FirstOrDefault(x => x.Id == id);
        //     if (developer == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _developers.Remove(developer);
        //     return Ok(developer);
        // }
    }
}