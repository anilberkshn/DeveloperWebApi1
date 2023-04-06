using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServerApi.Token
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        //LoginResponseModel üretecek metot.
        public string CreateAccessToken(string userName)
        {
            //Security  Key'in simetriğini alıyoruz.
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
 
            //Şifrelenmiş kimliği oluşturuyoruz.
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
 
            //Oluşturulacak loginResponseModel ayarlarını veriyoruz.
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: DateTime.Now.AddMinutes(5),//LoginResponseModel süresini 5 dk olarak belirliyorum
                notBefore: DateTime.Now,//LoginResponseModel üretildikten ne kadar süre sonra devreye girsin ayarlıyouz.
                claims: new []
                {
                    new Claim("userName", userName)
                },
                signingCredentials: signingCredentials
            );
 
            //LoginResponseModel oluşturucu sınıfında bir örnek alıyoruz.
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
 
            //LoginResponseModel üretiyoruz.
            return tokenHandler.WriteToken(securityToken);
        }
    }
}