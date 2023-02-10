using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DeveloperWepApi1.Model.Entities;
using Microsoft.IdentityModel.Tokens;

namespace DeveloperWepApi1.Token
{
    public class BuildToken
    {
        public string CreateToken()
        {
            var bytes = Encoding.UTF8.GetBytes("JwtKeyTokenKodu");
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
            SigningCredentials credentials =
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            JwtSecurityToken token = new JwtSecurityToken(
               // claims: claims,
                issuer: "http://localhost",
                audience: "http://localhost",
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);
            
            JwtSecurityTokenHandler tokenHandler= new JwtSecurityTokenHandler();

            
            return tokenHandler.WriteToken(token);

        }
    }
}