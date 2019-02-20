using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaaspopService.Persistence.Mappers;
using PaaspopService.Application.Infrastructure.Requests;
using PaaspopService.Persistence.Contexts;
using PaaspopService.Persistence.Settings;
using System.Reflection;

namespace PaaspopService
{
    // Basics architecture is inspired / used through: https://github.com/JasonGT/NorthwindTraders some things like requests, exceptions etc are directly coppied or directly used as inspiration.
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(validator => validator.RegisterValidatorsFromAssemblyContaining<Startup>());

            GeneralMapper.Map();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR();

            services.Configure<MongoDBSettings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoDB:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoDB:Database").Value;
            });

            services
                .AddSingleton<IDBContext, MongoDBContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } 
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
