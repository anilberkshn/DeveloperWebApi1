using System;
using System.Linq;
using System.Threading.Tasks;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.ErrorModels;
using DeveloperWepApi1.Model.RequestModels;
using DeveloperWepApi1.Model.ResponseModels;
using DeveloperWepApi1.Repository;
using DeveloperWepApi1.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperWepApi1.Controllers
{
      
    //[BasicAuthentication]
    [Route("api/developer")]
    [ApiController]
    [Authorize]
    public class DevelopersController : ControllerBase
    {
        private readonly IRepository _repository;
        //private readonly UserServiceJwt _userServiceJwt;

        public DevelopersController(IRepository repository)// , UserServiceJwt userServiceJwt
        {
            _repository = repository;
          // _userServiceJwt = userServiceJwt;
        } 
        
        [AllowAnonymous]
       [HttpGet("[action]")]       
        public IActionResult Login()
        {
            return Created("", new BuildToken().CreateToken());
        }

        //[AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult LoginSuccess()
        {
            return Ok("Login Success");
        }
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
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateDeveloper([FromBody] CreateDeveloperDto createDeveloperDto)
        {
            var developer = new Developer()
            {
                Id = Guid.NewGuid(),
                Name = createDeveloperDto.Name,
                Surname = createDeveloperDto.Surname,
                Department = createDeveloperDto.Department,
                Username = createDeveloperDto.Username,
                Password = createDeveloperDto.Password
            };
            _repository.InsertDeveloper(developer);

            var response = new DeveloperCreateResponse()
            {
                Id = developer.Id
            };
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult UpdateDeveloper(Guid developerId, [FromBody] UpdateDeveloperDto updateDeveloperDto)
        {
            var developer = _repository.GetById(developerId);
            _repository.UpdateDeveloper(developerId, updateDeveloperDto);
            return Ok(developer);
        }

        [AllowAnonymous]
        [HttpGet("{developerId}", Name = "developerId")]
        public IActionResult GetById(Guid developerId)
        {
            var findOne = _repository.GetById(developerId);
            return Ok(findOne);
        }
        
        
       // [BasicAuthentication]  
        [AllowAnonymous]
        [HttpGet("getAll")]  
        public IActionResult GetAll()
        {
            Console.WriteLine("getAll");
            var getAll = _repository.GetAll();
            return Ok(getAll);
        }
        
        [AllowAnonymous]
        [HttpDelete("{developerId}", Name = "developerId")]
        public IActionResult Delete(Guid id) // hard delete
        {
            var developer = _repository.GetById(id);
            _repository.Delete(developer.Id);
            return Ok(id);
        }
        [AllowAnonymous]
        [HttpPut("softDelete")]
        public IActionResult SoftDelete(Guid id, [FromBody] SoftDeleteDto softDeleteDto) // soft delete
        {
            var developer = _repository.GetById(id);
            _repository.SoftDelete(developer.Id, softDeleteDto);
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