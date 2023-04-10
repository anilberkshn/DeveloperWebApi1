using System;
using IdentityServerApi.Model.Entities;
using IdentityServerApi.Model.RequestModels;
using IdentityServerApi.Model.ResponseModel;
using IdentityServerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerApi.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/userIdentity")]
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
     
        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var findOne = _ıdentityService.GetById(id);
            return Ok(findOne);
        }

        [HttpPut("update")]
        public IActionResult Update(Guid userId, [FromBody] UpdateUserDto updateUserDto)
        {
            var user = _ıdentityService.GetById(userId);
            _ıdentityService.Update(userId, updateUserDto);
            return Ok(user);
        }
        
        [HttpDelete( "userId")]
        public IActionResult Delete(Guid id) 
        {
            var user = _ıdentityService.GetById(id);
            _ıdentityService.Delete(user.Id);
            return Ok(id);
        }
       
        [HttpPut("softDelete")]
        public IActionResult SoftDelete(Guid id, [FromBody] SoftDeleteDto softDeleteDto) // soft delete
        {
            var user = _ıdentityService.GetById(id);
            _ıdentityService.SoftDelete(user.Id, softDeleteDto);
            return Ok(id);
        }
        
        [HttpGet("GetAllUser")]  
         public IActionResult GetAll([FromQuery]GetAllDto getAllDto)
        {
            var getAllDeveloper = _ıdentityService.GetAllAsync(getAllDto); // todo: GetAll eklenecek
            return Ok(getAllDeveloper);
        }
    }
}