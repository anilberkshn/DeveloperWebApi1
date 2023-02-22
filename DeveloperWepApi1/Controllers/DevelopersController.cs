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
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TokenHandler = DeveloperWepApi1.Token.TokenHandler;

namespace DeveloperWepApi1.Controllers
{
    //[Authorize]   // mongo atlasta deneme yapmak için kaldırdım. 
    //[BasicAuthentication]
    [ApiController]
    [Route("api/developer")]
    //[Obsolete("Obsolete")]
    public class DevelopersController : ControllerBase
    {
       // readonly TokenContext _context;         // burası farklı 
        readonly IConfiguration _configuration;
        private readonly IDeveloperRepository _developerRepository;
        //private readonly UserServiceJwt _userServiceJwt;

        public DevelopersController(IDeveloperRepository developerRepository, IConfiguration configuration)// , UserServiceJwt userServiceJwt
        {
            _developerRepository = developerRepository;
            _configuration = configuration;
            // _userServiceJwt = userServiceJwt;
        } 
        
     //---------------------------------------------------------------
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
                Username = createDeveloperDto.Username,
                Password = createDeveloperDto.Password
            };
            _developerRepository.InsertDeveloper(developer);

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
            var developer = _developerRepository.GetById(developerId);
            _developerRepository.UpdateDeveloper(developerId, updateDeveloperDto);
            return Ok(developer);
        }
        
        // [AllowAnonymous]
        [HttpGet("{developerId}", Name = "developerId")]
        public IActionResult GetById(Guid developerId)
        {
            var findOne = _developerRepository.GetById(developerId);
            return Ok(findOne);
        }
        
        
       // [BasicAuthentication]  
        // [AllowAnonymous]
        [HttpGet("getAll")]  
        public IActionResult GetAll()
        {
            Console.WriteLine("getAll");
            var getAll = _developerRepository.GetAll();
            return Ok(getAll);
        }
        
        //[AllowAnonymous]
        [HttpDelete("{developerId}", Name = "developerId")]
        public IActionResult Delete(Guid id) // hard delete
        {
            var developer = _developerRepository.GetById(id);
            _developerRepository.Delete(developer.Id);
            return Ok(id);
        }
       //[AllowAnonymous]
        [HttpPut("softDelete")]
        public IActionResult SoftDelete(Guid id, [FromBody] SoftDeleteDto softDeleteDto) // soft delete
        {
            var developer = _developerRepository.GetById(id);
            _developerRepository.SoftDelete(developer.Id, softDeleteDto);
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