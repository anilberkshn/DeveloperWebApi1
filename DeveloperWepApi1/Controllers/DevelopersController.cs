using System;
using System.Collections.Generic;
using System.Linq;
using DeveloperWepApi1.Model;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;
using DeveloperWepApi1.Model.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperWepApi1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevelopersController : ControllerBase
    {
        public List<Developer> _developers { get; set; }

        public DevelopersController(List<Developer> developers)
        {
            _developers = developers;
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
            _developers.Add(developer);
 
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

        [HttpGet("SearchDeveloper")]
        public IActionResult SearchDeveloper([FromQuery] SearchDeveloperDto searchDeveloperDto)
        // bulamadı
        {
            var developers = _developers.Where(x =>
                x.Name.Contains(searchDeveloperDto.Name, StringComparison.OrdinalIgnoreCase));
            if (developers.Any())
            {
                return NotFound();
            }

            return Ok(developers);
        }

        [HttpPut("developerId:guid")]
        public IActionResult UpdateDeveloper(Guid developerId, [FromBody] UpdateDeveloperDto updateDeveloperDto)
        {
            var developer = _developers.FirstOrDefault(x => x.Id == developerId);
            if (developer == null)
            {
                return NotFound();
            }

            developer.Department = updateDeveloperDto.Department;
            developer.Name = updateDeveloperDto.Name;
            developer.Surname = updateDeveloperDto.Surname;

            return Ok();
        }
        //--------------------------------------------------------------------------------------

        [HttpGet("{developerId}", Name = "developerId")]
        public IActionResult GetById(Guid developerId)
        {
            var getId = _developers.FirstOrDefault(x => x.Id == developerId);
            return Ok(getId);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var getAll = _developers;
            return Ok(getAll);
        }

        /// <summary>
        /// Delete Developer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public IActionResult Delete(Guid id)
        {
            //  var developer  = _developers.Remove(_developers.FirstOrDefault(x => x.Id == id));
            var developer = _developers.FirstOrDefault(x => x.Id == id);
            if (developer == null)
            {
                return NotFound();
            }

            _developers.Remove(developer);
            return Ok(developer);
        }
    }
}