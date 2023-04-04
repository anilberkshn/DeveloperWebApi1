using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer1.Config;
using IdentityServer1.IdentityRepository;
using IdentityServer1.MongoDb.Context;
using IdentityServer1.MongoDb.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace IdentityServer1
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
      //  public Startup(IConfiguration configuration) => Configuration = configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            var dbSettings = Configuration.GetSection("UserDatabaseSettings").Get<MongoDbSettings>();
            var client = new MongoClient(dbSettings.ConnectionString);
            var context = new Context(client, dbSettings.DatabaseName);
            
           
            services.AddSingleton<IContext,Context>(_ => context);
            services.AddSingleton<IIdentityRepository,IdentityRepository.IdentityRepository>();
            
        }

        [Obsolete("Obsolete")]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvc(_ => _.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}"));
        }
    }
}