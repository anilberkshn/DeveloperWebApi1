using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using IdentityServerApi.Config;
using IdentityServerApi.Model.Entities;
using IdentityServerApi.Model.RequestModels;
using IdentityServerApi.Model.ResponseModel;
using IdentityServerApi.Services;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("api/userIdentity/CreateJwtToken")]
        public ActionResult Login([FromBody] LoginSettings model,GetAllDto getAllDto)
        {
            var userListDb = _identityService.GetAllAsync(getAllDto).GetAwaiter().GetResult().ToList();
            
            foreach (var user in userListDb)
            {
                if (model.Username == user.UserName && model.Password == user.Password)
                {
                    TokenHandler tokenHandler = new Token.TokenHandler(_configuration);
                    string token = tokenHandler.CreateAccessToken(model.Username);

                    return Ok(new LoginResponseModel
                    {
                        Success = true,
                        JwtToken = token
                    });
                }   
            }

            throw new NotImplementedException(); 
        }
    }
}