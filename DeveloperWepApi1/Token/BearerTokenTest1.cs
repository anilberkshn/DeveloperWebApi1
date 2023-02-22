// using System;
// using System.IdentityModel.Tokens.Jwt;
// using System.Net.Security;
// using System.Security.Claims;
// using System.Text;
// using System.Text.Encodings.Web;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Options;
// using Microsoft.IdentityModel.Tokens;
//
// namespace DeveloperWepApi1.LoginResponseModel
// {
//     public class BearerTokenTest1 : AuthenticationHandler<AuthenticationSchemeOptions>
//     {
//         public BearerTokenTest1(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
//             UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
//         {
//         }
//
//         protected override Task<JwtSecurityTokenHandler> HandleAuthenticateAsync()
//         {
//             // Kullanıcı bilgileri
//             var username = "user1";
//             var userId = 123;
//
//             // LoginResponseModel oluşturma ayarları
//             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret_key_for_signing"));
//             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//             var claims = new[]
//             {
//                 new Claim(ClaimTypes.Name, username),
//                 new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
//             };
//
//             // LoginResponseModel oluşturma
//             var token = new JwtSecurityToken(
//                 issuer: "issuer_name",
//                 audience: "audience_name",
//                 claims: claims,
//                 expires: DateTime.Now.AddMinutes(30),
//                 signingCredentials: creds
//             );
//
//             var bearerToken = new JwtSecurityTokenHandler().WriteToken(token);
//
//             // Kullanıcıya token dön
//             return bearerToken;
//         }
//     }
// }