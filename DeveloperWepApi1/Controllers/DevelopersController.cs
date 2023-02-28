using System;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;
using DeveloperWepApi1.Model.ResponseModels;
using DeveloperWepApi1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperWepApi1.Controllers
{
    // [Authorize] // denemeler sırasında her seferinde giriş yapmamak için.
    [ApiController]
    [Route("api/developer")]
    public class DevelopersController : ControllerBase
    {
        private readonly IDeveloperService _developerService;
        public DevelopersController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }
        //[AllowAnonymous]
        [HttpPost]
        public IActionResult CreateDeveloper([FromBody] CreateDeveloperDto createDeveloperDto)
        {
            var developer = new Developer()
            {
                Id = Guid.NewGuid(),
                Name = createDeveloperDto.Name,
                Surname = createDeveloperDto.Surname,
                Department = createDeveloperDto.Department,
                // Username = createDeveloperDto.Username,
                // Password = createDeveloperDto.Password
            };
            _developerService.InsertDeveloperAsync(developer);

            var response = new DeveloperCreateResponse()
            {
                Id = developer.Id
            };
            return Ok(response);
        }
        //[AllowAnonymous]
        [HttpPut]
        public IActionResult UpdateDeveloper(Guid developerId, [FromBody] UpdateDeveloperDto updateDeveloperDto)
        {
            var developer = _developerService.GetById(developerId);
            _developerService.UpdateDeveloper(developerId, updateDeveloperDto);
            return Ok(developer);
        }
        
        // [AllowAnonymous]
        [HttpGet("{developerId}", Name = "developerId")]
        public IActionResult GetById(Guid developerId)
        {
            var findOne = _developerService.GetById(developerId);
            return Ok(findOne);
        }

      //   [AllowAnonymous]
        [HttpGet("GetAllDeveloper")]  
        public IActionResult GetAll(string name)
        {
            var getAllDeveloper = _developerService.GetAllAsync(name);
            // var getAll = _developerService.GetAllAsync();
            return Ok(getAllDeveloper);
        }
        
        //[AllowAnonymous]
        [HttpDelete("{developerId}", Name = "developerId")]
        public IActionResult Delete(Guid id) // hard delete
        {
            var developer = _developerService.GetById(id);
            _developerService.Delete(developer.Id);
            return Ok(id);
        }
       //[AllowAnonymous]
        [HttpPut("softDelete")]
        public IActionResult SoftDelete(Guid id, [FromBody] SoftDeleteDto softDeleteDto) // soft delete
        {
            var developer = _developerService.GetById(id);
            _developerService.SoftDelete(developer.Id, softDeleteDto);
            return Ok(id);
        }
        
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


//////----------------------------------------------------
// //  [AllowAnonymous]
// [HttpGet("[action]")]       
//  public IActionResult Login()
//  {
//      return Created("", new BuildToken().CreateToken());
//      //"JwtKeyTokenKodu"
//  }
//
//  //[AllowAnonymous]
//  [Authorize]
//  [HttpGet("[action]")]
//  public IActionResult LoginSuccess()
//  {
//      return Ok("Login Success");
//  }
//[AllowAnonymous]
// [HttpPost("authenticate")]
// public IActionResult Authenticate([FromBody] Developer user)
//     //[FromHeader]
// {
//
//     var token = _userServiceJwt.AuthenticateService(user);
//
//     if (token == null)
//     {
//         return Unauthorized();
//     }
//
//     return Ok(new {token,user});
//    // return Ok(new {user, user });
// }