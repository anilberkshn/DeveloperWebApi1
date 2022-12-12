using System;
using System.Collections.Generic;
using System.Linq;
using DeveloperWepApi1.Model.Entities;
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

        [HttpPost("AddDeveloper")]
        public IActionResult AddDeveloper(Developer developer)
        {
            Developers.Add(developer);
            return Ok(developer.Id);
        }
        
        [HttpGet("getById")]
        public IActionResult GetById(Guid id)
        {
            var getId = Developers.FirstOrDefault(x => x.Id == id);
            return Ok(getId);
        }
        
        [HttpGet ("getAll")]
        public IActionResult GetAll()
        {
            var getAll = Developers;
            return Ok(getAll);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Guid id)
        {
            var delete = Developers.Remove(Developers.FirstOrDefault(x => x.Id == id));
            
            return Ok(delete);
        }
        
        
        
    }
}