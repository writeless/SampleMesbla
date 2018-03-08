using AutoMapper;
using FluentValidation;
using MediatR;
using Mesbla.Core.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using Writeless.Exceptions;
using Writeless.Extensions.AutoMapper;
using Writeless.Extensions.Swagger;
using Writeless.Pipelines;

//TODO: ver se tem algo interessante a implementar
//https://github.com/gothinkster/aspnetcore-realworld-example-app/blob/2fb44f214b874f6007b6bc7b8da860de9385170b/src/Conduit/Startup.cs

namespace Mesbla.WebApi
{
    public class Startup
    {
        public readonly string ApiTitle = "Mesbla";
        public readonly string[] ApiVersions = new string[] { "v1" };

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //FluenteValidation
            var assembliesToRegister = new List<Assembly>() { typeof(BaseContext).Assembly };
            AssemblyScanner.FindValidatorsInAssemblies(assembliesToRegister)
                .ForEach(pair =>
                    services.Add(ServiceDescriptor.Transient(pair.InterfaceType, pair.ValidatorType))
                );

            //AutoMapper
            Action<IMapperConfigurationExpression> autoMapperConfig = config => 
                config.ForAllMaps((type, map) => 
                    map.IgnoreAllPropertiesWithAnInaccessibleSetter(type)
                );            
            services.AddAutoMapper(autoMapperConfig, GetType().Assembly);

            //MediatR  
            services.AddMediatR();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

            //Swagger
            services.AddSwaggerGen(ApiTitle, ApiVersions);

            //DbContext 
            var assemblyName = GetType().Assembly.GetName().Name;
            Action<SqlServerDbContextOptionsBuilder> dbOptionsBuilder = options => {
                options.MigrationsAssembly(assemblyName);
                options.MaxBatchSize(100);
            };

            var commandConnectionString = Configuration.GetConnectionString("CommandConnectionString");
            var queryConnectionString = Configuration.GetConnectionString("QueryConnectionString");

            Action<DbContextOptionsBuilder> commandDbOptions = options => options.UseSqlServer(commandConnectionString, dbOptionsBuilder);
            Action<DbContextOptionsBuilder> queryDbOptions = options => options.UseSqlServer(queryConnectionString);            

            services.AddDbContext<CommandContext>(commandDbOptions);
            services.AddDbContext<QueryContext>(queryDbOptions);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CommandContext commandContext)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
                loggerFactory.AddConsole(LogLevel.Debug);

                app.UseExceptionHandler(AppExceptionHandler.InDev);

                commandContext.Database.EnsureCreated();

                app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            }
            else
            {
                app.UseExceptionHandler(AppExceptionHandler.InProd);

                //TODO: app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
            }

            app.UseMvc();

            //Swagger
            app.UseSwaggerUI(ApiTitle, ApiVersions);
        }
    }
}
