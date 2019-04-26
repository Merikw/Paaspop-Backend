using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Application.Infrastructure.Requests;
using PaaspopService.Application.Infrastructure.Validators;
using PaaspopService.Application.Performances.Commands.UpdatePerformance;
using PaaspopService.Application.Performances.Queries.GetFavoritePerformancesFromUser;
using PaaspopService.Application.Performances.Queries.GetPerformances;
using PaaspopService.Application.Performances.Queries.GetPerformancesById;
using PaaspopService.Application.Places.Commands.UpdatePlace;
using PaaspopService.Application.Places.Queries.GetBestPlacesQuery;
using PaaspopService.Application.Places.Queries.GetMeetingPointQuery;
using PaaspopService.Application.Places.Queries.GetPlacesQuery;
using PaaspopService.Application.Users.Commands.CreateUser;
using PaaspopService.Application.Users.Commands.RemoveUser;
using PaaspopService.Application.Users.Commands.UpdateUser;
using PaaspopService.Common.Middleware;
using PaaspopService.Domain.Entities;
using PaaspopService.Persistence.Contexts;
using PaaspopService.Persistence.Mappers;
using PaaspopService.Persistence.Repositories;
using PaaspopService.Persistence.Settings;
using Swashbuckle.AspNetCore.Swagger;

namespace PaaspopService.WebApi
{
    // Basics architecture is inspired / used through: https://github.com/JasonGT/NorthwindTraders some things like requests, exceptions etc are directly coppied or directly used as inspiration.
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the cont ainer.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(validator =>
                    validator.RegisterValidatorsFromAssemblyContaining<GetArtistValidator>());

            services.AddAutoMapper();
            services.AddMediatR(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Paaspop service API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services
                .AddSingleton<IDbContext, MongoDbContext>()
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                .AddScoped<IRequestHandler<GetPerformancesQuery, PerformanceViewModel>, GetPerformancesQueryHandler>()
                .AddScoped<IRequestHandler<GetPerformanceByIdQuery, Performance>, GetPerformanceByIdHandler>()
                .AddScoped<IRequestHandler<UpdatePerformanceCommand, Performance>, UpdatePerformanceHandler>()
                .AddScoped<IRequestHandler<GetFavoritePerformancesFromUserQuery, List<Performance>>,
                    GetFavoritePerformancesFromUserHandler>()
                .AddScoped<IRequestHandler<CreateUserCommand, User>, CreateUserHandler>()
                .AddScoped<IRequestHandler<UpdateUserCommand, User>, UpdateUserHandler>()
                .AddScoped<IRequestHandler<RemoveUserCommand, Unit>, RemoveUserHandler>()
                .AddScoped<IRequestHandler<UpdatePlaceCommand, Place>, UpdatePlaceHandler>()
                .AddScoped<IRequestHandler<GetBestPlacesQuery, BestPlacesViewModel>, GetBestPlacesHandler>()
                .AddScoped<IRequestHandler<GetPlacesQuery, List<Place>>, GetPlacesHandler>()
                .AddScoped<IRequestHandler<GetMeetingPointQuery, Place>, GetMeetingPointHandler>()
                .AddTransient<IArtistsRepository, ArtistsRepositoryMongoDb>()
                .AddTransient<IUsersRepository, UsersRepositoryMongoDb>()
                .AddTransient<IPerformancesRepository, PerformancesRepositoryMongoDb>()
                .AddTransient<IPlacesRepository, PlacesRepositoryMongoDb>();

            if (!BsonClassMap.IsClassMapRegistered(typeof(GeneralMapper)))
                try
                {
                    BsonClassMap.RegisterClassMap<GeneralMapper>();
                }
                catch (ArgumentException)
                {
                    // ignored
                }

            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString =
                    Environment.GetEnvironmentVariable(Configuration.GetSection("MongoDbEnv:ConnectionString").Value);
                options.Database =
                    Environment.GetEnvironmentVariable(Configuration.GetSection("MongoDbEnv:Database").Value);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Paaspop service API");
                c.RoutePrefix = string.Empty;
            });
            app.UseMvc();
        }
    }
}