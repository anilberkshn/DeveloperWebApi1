using System;
using System.Collections.Generic;
using System.Net;
using IdentityServerApi.Config;
using IdentityServerApi.Model.RequestModels;
using IdentityServerApi.Model.ResponseModel;
using IdentityServerApi.Services;
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

        [HttpPost("action")]
        public ActionResult Login([FromBody] LoginSettings model)
        {
            var userList = _configuration.GetSection("Users").Get<List<LoginSettings>>();

            foreach (var user in userList)
            {
                if (model.Username == user.Username && model.Password == user.Password)
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