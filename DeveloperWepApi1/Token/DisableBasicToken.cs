// using System;
// using System.Linq;
// using System.Net.Http.Headers;
// using System.Security.Claims;
// using System.Text;
// using System.Text.Encodings.Web;
// using System.Threading.Tasks;
// using DeveloperWepApi1.Model.Entities;
// using DeveloperWepApi1.Model.RequestModels;
// using DeveloperWepApi1.DeveloperRepository;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Options;
//
// namespace DeveloperWepApi1.LoginResponseModel
// {
//     public class BasicToken  : AuthenticationHandler<AuthenticationSchemeOptions>
//     {
//          private readonly IDeveloperRepository _developerRepository;
//         // readonly Users _users;
//
//         public BasicToken(IOptionsMonitor<AuthenticationSchemeOptions> options,
//             ILoggerFactory logger,
//             UrlEncoder encoder,
//             ISystemClock clock,
//             IDeveloperRepository developerRepository)
//             : base(options, logger, encoder, clock)
//         {
//             _developerRepository = developerRepository;
//          //   _users = users;
//         }
//
//         protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
//         {
//            // string usarname = null;
//            var endpoint = Context.GetEndpoint();
//             if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
//                 return AuthenticateResult.NoResult();
//
//             if (!Request.Headers.ContainsKey("Authorization"))
//                 return AuthenticateResult.Fail("Missing Authorization Header");
//
//             Developer developer = null;
//             try
//             {
//                 var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
//                 var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
//                 var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
//
//                 // usarname = credentials.FirstOrDefault();
//                 // var password = credentials.LastOrDefault();
//                 var username = credentials[0];
//                 var password = credentials[1];
//                 developer = await _developerRepository.Authenticate(null);
//                 //developer = await _developerRepository.Authenticate(new AuthenticateModel());
//                 // if (!_users.ValidateCredentials(usarname,password))   // bu kısımda eşleştirmeyı yanlış yapmışım.
//                 // {
//                 //     throw new ArgumentException("invalid credentials");
//                 // }
//             }
//             catch 
//             {
//                 return AuthenticateResult.Fail($"Authentication failed");  
//             }
//
//             if (developer == null)
//                 return AuthenticateResult.Fail("Invalid username or Password");
//
//             var claims = new[] {
//                 new Claim(ClaimTypes.NameIdentifier, developer.Id.ToString()),
//                 new Claim(ClaimTypes.Name, developer.username),
//             };
//            
//             var identity = new ClaimsIdentity(claims, Scheme.Name);
//             var principal = new ClaimsPrincipal(identity);
//             var ticket = new AuthenticationTicket(principal, Scheme.Name);
//
//             return AuthenticateResult.Success(ticket);
//         }
//
//         protected override Task HandleChallengeAsync(AuthenticationProperties properties)
//         {
//             Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
//             return base.HandleChallengeAsync(properties);
//         }
//     }
// }
//
//
//  