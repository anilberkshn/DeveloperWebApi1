using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DeveloperWepApi1.Model.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace DeveloperWepApi1.Token
{
    public class UserServiceJwt
    {
        private readonly IMongoCollection<Developer> _userJwt;

        private readonly string _key;

        public UserServiceJwt(IConfiguration configuration)
        {
            var client =new MongoClient(configuration.GetConnectionString("Developers"));

            var database = client.GetDatabase("DevelopersDb");

            _userJwt = database.GetCollection<Developer>("Developers");

            this._key = configuration.GetSection("JwtKey").ToString();

            //.GetSection("DeveloperDatabaseSettings").Get<DeveloperDatabaseSettings>());
            //     //new MongoClient(dbSettings.ConnectionString);
            // var context = new Context(client, dbSettings.DatabaseName);
           
        }
        public string AuthenticateService(User dev)
        {
            var developer = 
                this._userJwt.Find(x => x.Username == dev.Username&& x.Password ==dev.Password)
                    .FirstOrDefault()
                    .ToString();
            
            if (developer == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, dev.Username) // Bu kısım farklı 
                }),
               
                Expires = DateTime.UtcNow.AddHours(1),
               
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), 
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}