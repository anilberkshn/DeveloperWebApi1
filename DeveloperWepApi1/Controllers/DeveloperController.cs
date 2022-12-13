using System;
using System.Collections.Generic;
using System.Linq;
using DeveloperWepApi1.Model;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperWepApi1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class DeveloperController : ControllerBase
    {
        public List<Developer> Developers { get; set; }
        public DeveloperController(List<Developer> developers)
        {
            Developers = developers;
        }

        [HttpPost("CreateDeveloper")]
        public IActionResult CreateDeveloper([FromBody] CreateDeveloperDto createDeveloperDto)
        {
            var developer = new Developer()
            {
                Id = Guid.NewGuid(),
                Name = createDeveloperDto.Name,
                Surname = createDeveloperDto.Surname
            };
            Developers.Add(developer);

            var response = new DeveloperCreateResponse()
            {
                Id = developer.Id
            };
            return Ok(response);
        }
        // [HttpPost("AddDeveloper")]
        // public IActionResult AddDeveloper(Developer developer)
        // {
        //     Developers.Add(developer);
        //     return Ok(developer.Id);
        // }
        [HttpGet("{developerId}",Name = "developerId")]
        public IActionResult GetById(Guid developerId)
        {
            var getId = Developers.FirstOrDefault(x => x.Id == developerId);
            return Ok(getId);
        }

        [HttpPut("UpdateDeveloper + {developerId:guid}")]
        public IActionResult UpdateDeveloper(Guid developerId, [FromBody] UpdateDeveloperDto updateDeveloperDto)
        {
            
            return Ok();
        }
        
        [HttpGet ("getAll")]
        public IActionResult GetAll()
        {
            var getAll = Developers;
            return Ok(getAll);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Guid id)
        {
            var delete = Developers.Remove(Developers.FirstOrDefault(x => x.Id == id));
            
            return Ok(delete);
        }
        
        
        
    }
}