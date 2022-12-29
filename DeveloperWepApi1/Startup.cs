using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeveloperWepApi1.Config;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Mongo.Context;
using DeveloperWepApi1.Mongo.Interface;
using DeveloperWepApi1.Repository;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
          
            //var dbSettings = services.BuildServiceProvider().GetService<DeveloperDatabaseSettings>();
           
            var client = new MongoClient("mongodb://localhost:27017");
            var context = new Context(client, "DeveloperDb");

            services.AddSingleton<IContext, Context>(provider => context);
            services.AddSingleton<IRepository, Repository.Repository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP requ   est pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeveloperWepApi1 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}