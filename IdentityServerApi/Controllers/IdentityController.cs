using System;
using IdentityServerApi.Model.Entities;
using IdentityServerApi.Model.RequestModels;
using IdentityServerApi.Model.ResponseModel;
using IdentityServerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerApi.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _ıdentityService;

        public IdentityController(IIdentityService ıdentityService)
        {
            _ıdentityService = ıdentityService;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var user = new UserProperties()
            {
                Id = new Guid(),
                Name = createUserDto.Name,
                Surname = createUserDto.Surname,
                UserName = createUserDto.UserName,
                Email = createUserDto.Email,
                Password = createUserDto.Password
            };
            _ıdentityService.InsertAsync(user);

            var response = new CreateUserResponse()
            {
                Id = user.Id
            };
            return Ok(response);
        }
        [HttpGet("{userId}", Name = "userId")]
        public IActionResult GetById(Guid uGuid)
        {
            var findOne = _ıdentityService.GetById(uGuid);
            return Ok(findOne);
        }
        
        
        // [HttpGet("GetAllUser")]  
        // public IActionResult GetAll([FromQuery]GetAllDto getAllDto)
        // {
        //     var getAllDeveloper = _ıdentityService.GetAllAsync(getAllDto); // todo: GetAll eklenecek
        //     return Ok(getAllDeveloper);
        // }
    }
}