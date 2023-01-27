using System;
using DeveloperWepApi1.Config;
using DeveloperWepApi1.Middlewares;
using DeveloperWepApi1.Mongo.Context;
using DeveloperWepApi1.Mongo.Interface;
using DeveloperWepApi1.Repository;
using DeveloperWepApi1.Token;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Owin.Security.OAuth;
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DeveloperWepApi1", Version = "v1" });
               
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme  
                {  
                    Name = "Authorization",  
                    Type = SecuritySchemeType.Http,  
                    Scheme = "basic",  
                    In = ParameterLocation.Header,  
                    Description = "Basic Authorization header using the Bearer scheme."  
                });  
                c.AddSecurityRequirement(new OpenApiSecurityRequirement  
                {  
                    {  
                        new OpenApiSecurityScheme  
                        {  
                            Reference = new OpenApiReference  
                            {  
                                Type = ReferenceType.SecurityScheme,  
                                Id = "basic"  
                            }  
                        },  
                        new string[] {}  
                    }  
                });
                
                
            });

            var dbSettings = Configuration.GetSection("DeveloperDatabaseSettings").Get<DeveloperDatabaseSettings>();
            var client = new MongoClient(dbSettings.ConnectionString);
            var context = new Context(client, dbSettings.DatabaseName);
            
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicToken>("BasicAuthentication", null);


            services.AddSingleton<IContext, Context>(_ => context); // provider kullanılmaması
            services.AddSingleton<IRepository, Repository.Repository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //  app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeveloperWepApi1 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<NumberOneMiddleware>();
            app.UseMiddleware<NumberTwoMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            // OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions()
            // {
            //     TokenEndpointPath = new Microsoft.Owin.PathString("/token"), // token alacağımız path'i belirtiyoruz
            //     AccessTokenExpireTimeSpan = TimeSpan.FromHours(1), //token expire süresini ayarlıyoruz Örn : 1 saat
            //     AllowInsecureHttp = true,
            //     Provider = new AuthorizationServerProvider()
            // };        

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}