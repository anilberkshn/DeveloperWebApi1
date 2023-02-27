using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using TaskWepApi1.Config;
using TaskWepApi1.Database.Context;
using TaskWepApi1.Database.Interface;
using TaskWepApi1.Middlewares;
using TaskWepApi1.Repository;
using TaskWepApi1.Services;
using TaskWepApi1.Validation;

namespace TaskWepApi1
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
        {
            services.AddControllers().AddFluentValidation(
                fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
               // fv => fv.RegisterValidatorsFromAssemblyContaining<TaskValidator>());
            services.AddControllers();
          
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskWepApi1", Version = "v1" });
            });

            var dbSettings = Configuration.GetSection("TaskDatabaseSettings").Get<TaskDatabaseSettings>();
            var client = new MongoClient(dbSettings.ConnectionString);
            var context = new Context(client,dbSettings.DatabaseName);
            
            services.AddSingleton<ITaskService, TaskWepApi1.Services.TaskService>();
            services.AddSingleton<IContext, Context>(_ => context);
            services.AddSingleton<IRepository, Repository.Repository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
               // app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskWepApi1 v1"));
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}



// ******************** Eski taskı liste tutuğumuz hali singleton içinde
//
// services.AddSingleton(
// new List<TaskModel.TaskEntities.Task>()
// {
//     new (){Id = Guid.NewGuid(),Department = "Backend",Description = "ses dneeme 123",
//         DeveloperId = Guid.NewGuid(),Status = 1,Title = "Çalışıyor mu"},
//                     
//     new (){Id = Guid.NewGuid(),Department = "Frontend",Description = "Frontend 123",
//         DeveloperId = Guid.NewGuid(),Status = 1,Title = "Frontend"},
//                     
//     new (){Id = Guid.NewGuid(),Department = "dizi",Description = "Arka sokaklar 213414243 bolum ", 
//         DeveloperId = Guid.NewGuid(),Status = 1,Title = "Arka sokaklar"},
//                     
//     new (){Id = Guid.NewGuid(),Department = "dizi",Description = "senin ağzından çıkanla kulağının tuttuğunun duyduğu bir mi?",
//         DeveloperId = Guid.NewGuid(),Status = 1,Title = "Leyla ile mecnun"},
//                     
//     new (){Id = Guid.NewGuid(),Department = "film",Description = "dnememememkeraşsdjdjdlokdfoıhasdfaıodfsdfnhfdsnhaoıhj", 
//         DeveloperId = Guid.NewGuid(),Status = 1,Title = "Deneme"},
//
// }
// );