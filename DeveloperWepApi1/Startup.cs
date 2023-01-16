using System;
using DeveloperWepApi1.Config;
using DeveloperWepApi1.Middlewares;
using DeveloperWepApi1.Mongo.Context;
using DeveloperWepApi1.Mongo.Interface;
using DeveloperWepApi1.Repository;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete("Obsolete")]
        public void ConfigureServices(IServiceCollection services)
        {               // fluent  validation denemesi aşağıda.
            services.AddControllersWithViews().AddFluentValidation
                (x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
                //  validator sınıflarını buldurmak için kullanılan 
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DeveloperWepApi1", Version = "v1" });
            });
            
            var dbSettings =  Configuration.GetSection("DeveloperDatabaseSettings").Get<DeveloperDatabaseSettings>();
            //var dbSettings = services.BuildServiceProvider().GetService<DeveloperDatabaseSettings>();
            var client = new MongoClient(dbSettings.ConnectionString);
            var context = new Context(client,dbSettings.DatabaseName);

            services.AddSingleton<IContext, Context>(_ => context); // provider kullanılmaması
            services.AddSingleton<IRepository, Repository.Repository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP requ   est pipeline.
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
            // log, exception, yetki
            app.UseMiddleware<NumberOneMiddleware>();   // yukarıdan aşağı aşağıdan da yukarı.
            app.UseMiddleware<NumberTwoMiddleware>();   // yukarıdan aşağı aşağıdan da yukarı.
            app.UseMiddleware<ErrorHandlingMiddleware>();
          
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}