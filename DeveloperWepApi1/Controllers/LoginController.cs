using System.Linq;
using System.Threading.Tasks;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;
using DeveloperWepApi1.Repository;
using DeveloperWepApi1.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DeveloperWepApi1.Controllers
{
    public class LoginController : ControllerBase
    {
        readonly IConfiguration _configuration;
        private readonly IRepository _repository;
        
        public LoginController(IRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        
        [HttpPost("action")]
        public async Task<DeveloperWepApi1.Model.Entities.Token> Login([FromForm]UpdateDeveloperDto developerLogin)
        {
            var developers = _repository.GetAll().GetAwaiter().GetResult();
            Developer developer = developers.FirstOrDefault(x => x.Username == developerLogin.Username && x.Password == developerLogin.Password);
            if (developer != null)
            {
                //Token üretiliyor.
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                DeveloperWepApi1.Model.Entities.Token token = tokenHandler.CreateAccessToken(developer);
 
                //Refresh token Users tablosuna işleniyor.
                developer.RefreshToken = token.RefreshToken;
                developer.RefreshTokenEndDate = token.Expiration.AddMinutes(3);
                _repository.UpdateDeveloper(developer.Id,developerLogin);
 
                return token;
            }
            return null;
        }
    }
}