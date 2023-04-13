using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using IdentityServerApi.Config;
using IdentityServerApi.Model.Entities;
using IdentityServerApi.Model.RequestModels;
using IdentityServerApi.Model.ResponseModel;
using IdentityServerApi.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TokenHandler = IdentityServerApi.Token.TokenHandler;

namespace IdentityServerApi.Controllers
{
    public class LoginController : ControllerBase
    {
        readonly IConfiguration _configuration;
        private readonly IIdentityService _identityService;

        public LoginController(IIdentityService identityService, IConfiguration configuration)
        {
            _identityService = identityService;
            _configuration = configuration;
        }

        [HttpPost("api/CreateJwtToken")]
        public ActionResult Login([FromBody] LoginSettings loginModel,GetAllDto getAllDto)
        {
            // var getALlDto = new GetAllDto(0,15);
            var userList = _configuration.GetSection("Users").Get<List<LoginSettings>>();
            var userListDb = _identityService.GetAllAsync(getAllDto).GetAwaiter().GetResult().ToList();
            
            foreach (var user in userListDb)
            {
                if (loginModel.Username == user.UserName && loginModel.Password == user.Password)
                {
                    TokenHandler tokenHandler = new Token.TokenHandler(_configuration);
                    string token = tokenHandler.CreateAccessToken(loginModel.Username);

                    return Ok(new LoginResponseModel
                    {
                        Success = true,
                        JwtToken = token
                    });
                }   
            }

            throw new NotImplementedException(); 
        }
        //----------------------------------------------------------------------------------
        
        
        [HttpPost("api/CreateJwtToken2")]

        public IActionResult Post([FromBody] LoginSettings loginModel,GetAllDto getAllDt)
        {
            if (IsValidUser(loginModel,getAllDt))
            {
                var token = GenerateToken(loginModel.Username);
                return Ok(token);
            }

            return Unauthorized();
        }
        
        private string GenerateToken(string userName)  // beninm yukarıdakinde CreateAccessToken
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }
        
        private bool IsValidUser([FromBody] LoginSettings loginModel,GetAllDto getAllDto)
        {
            var userListDb = _identityService.GetAllAsync(getAllDto).GetAwaiter().GetResult().ToList();

            foreach (var user in userListDb)
            {
                if (loginModel.Username == user.UserName && loginModel.Password == user.Password)
                {
                  return true;
                }   
            }
            return false; 
            
        }

    }
}