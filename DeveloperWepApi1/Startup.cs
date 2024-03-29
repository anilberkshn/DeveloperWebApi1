using System;
using System.Text;
using Confluent.Kafka;
using DeveloperWepApi1.Config;
using DeveloperWepApi1.DeveloperRepository;
using DeveloperWepApi1.Extensions;
using DeveloperWepApi1.Kafka;
using DeveloperWepApi1.Middlewares;
using DeveloperWepApi1.Mongo.Context;
using DeveloperWepApi1.Mongo.Interface;
using DeveloperWepApi1.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;    
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace DeveloperWepApi1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [Obsolete("Obsolete")]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddFluentValidation
                (x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
            //  validator sınıflarını buldurmak için kullanılan 
           
            services.AddControllers();
            services.AddMySwagger();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,  //sonradan
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration["Token:Issuer"], //"https://localhost",
                    ValidAudience =  Configuration["Token:Audience"], // "https://localhost", //         ValidAudience = "https://localhost:5001",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])), // token kodu "JwtKeyTokenKodu"
                    ClockSkew = TimeSpan.Zero
                };
                services.AddControllers();
            });
         
            services.AddControllers();
         
            
            var dbSettings = Configuration.GetSection("DeveloperDatabaseSettings").Get<DeveloperDatabaseSettings>();
            var client = new MongoClient(dbSettings.ConnectionString);
            var context = new Context(client, dbSettings.DatabaseName);
            
            services.AddSingleton<IDeveloperService, DeveloperService>();
            services.AddSingleton<IContext, Context>(_ => context); // provider kullanılmaması
            services.AddSingleton<IDeveloperRepository, DeveloperRepository.DeveloperRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //  app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json", "DeveloperWepApi1 v1"));
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();//app.useErrorHandling yazımı Extension ile
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
    }
}


//___________________Basic________________________________
// services.AddAuthentication("BasicAuthentication")  
//     .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);  
//------------------JWT test1-------------------
// services.AddAuthentication(x =>
// {
//     x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(x =>
// {
//     x.RequireHttpsMetadata = false;
//     x.SaveToken = true;
//     x.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuerSigningKey = true,
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("JwtKey").ToString() ?? throw new InvalidOperationException())),
//         ValidateIssuer = false,
//         ValidateAudience = false
//     };
// });
//----------------------------------

// app.UseMiddleware<NumberOneMiddleware>();
// app.UseMiddleware<NumberTwoMiddleware>();


/////////*************
// basic authenticationu kapattım. // swagger.gen içinde idi
// c.AddSecurityDefinition("basic", new OpenApiSecurityScheme  
// {  
//     Name = "Authorization",  
//     Type = SecuritySchemeType.Http,  
//     Scheme = "basic",  
//     In = ParameterLocation.Header,  
//     Description = "Basic Authorization header using the Bearer scheme."  
// });  
// c.AddSecurityRequirement(new OpenApiSecurityRequirement  
// {  
//     {  
//         new OpenApiSecurityScheme  
//         {  
//             Reference = new OpenApiReference  
//             {  
//                 Type = ReferenceType.SecurityScheme,  
//                 Id = "basic"  
//             }  
//         },  
//         new string[] {}  
//     }  
// });
