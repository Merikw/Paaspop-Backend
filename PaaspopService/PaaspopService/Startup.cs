﻿using System;
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
using PaaspopService.Common.Middleware;
using PaaspopService.Persistence.Contexts;
using PaaspopService.Persistence.Mappers;
using PaaspopService.Persistence.Repositories;
using PaaspopService.Persistence.Settings;

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

            services
                .AddSingleton<IDbContext, MongoDbContext>()
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                .AddTransient<IArtistsRepository, ArtistsRepositoryMongoDb>()
                .AddTransient<IUsersRepository, UsersRepositoryMongoDb>()
                .AddTransient<IPerformancesRepository, PerformancesRepositoryMongoDb>()
                .AddMediatR();

            if (!BsonClassMap.IsClassMapRegistered(typeof(GeneralMapper)))
                try
                {
                    BsonClassMap.RegisterClassMap<GeneralMapper>();
                }
                catch (ArgumentException exception)
                {
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
            app.UseMvc();
        }
    }
}