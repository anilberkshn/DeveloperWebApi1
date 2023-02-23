using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DeveloperWepApi1.Config;
using DeveloperWepApi1.DeveloperRepository;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.ErrorModels;
using DeveloperWepApi1.Model.RequestModels;
using DeveloperWepApi1.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DeveloperWepApi1.Controllers
{
    public class LoginController : ControllerBase
    {
        readonly IConfiguration _configuration;
        private readonly IDeveloperRepository _developerRepository;

        public LoginController(IDeveloperRepository developerRepository, IConfiguration configuration)
        {
            _developerRepository = developerRepository;
            _configuration = configuration;
        }

        [HttpPost("action")]
        public ActionResult Login([FromBody] LoginRequestModel model)
        {
            var userList = _configuration.GetSection("Users").Get<List<UserSettings>>();

            foreach (var user in userList)
            {
                if (model.Username == user.Username && model.Password == user.Password)
                {
                    TokenHandler tokenHandler = new TokenHandler(_configuration);
                    string token = tokenHandler.CreateAccessToken(model.Username);

                    return Ok(new LoginResponseModel
                    {
                        Success = true,
                        JwtToken = token
                    });
                }   
            }

            throw new DeveloperException(HttpStatusCode.Unauthorized,"Kullanci adi veya sifre hatali");
        }
    }
}