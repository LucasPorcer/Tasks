using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Tasks.API.Domain.Notifications;
using Tasks.API.InfraData.Database.Tasks;
using Tasks.API.Application.Services.Commands.Tasks;
using Tasks.API.Domain.Interfaces.Repository.Tasks;
using Tasks.API.InfraData.Repositories.Tasks;
using System.IO;
using System.Reflection;
using System;

namespace Tasks.API.UI
{
    public class Startup
    { 
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
       
        public void ConfigureServices(IServiceCollection services)
        {
            AddDbContext(services);

            AddRepositories(services);

            AddMediatR(services);

            services.AddTransient<DomainNotification>();

            services.AddLogging();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tasks.API.UI", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
        }

        private void AddMediatR(IServiceCollection services)
        {
            services.AddMediatR(typeof(Program).Assembly, typeof(TasksCommandHandler).Assembly);
        }

        private void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<ITaskRepository, TaskRepository>();
        }

        private void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<TasksDbContext>(opt => opt.UseInMemoryDatabase("tasksDb"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasks.API.UI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
