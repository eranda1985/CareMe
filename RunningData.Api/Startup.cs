using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RunningData.Api.ActionFilters;
using RunningData.Api.Exceptions;
using RunningData.Core;
using RunningData.Model.DataConnections;
using RunningData.Model.Dto;
using RunningData.Model.Repositories;
using RunningData.Model.Repositories.Interfaces;
using RunningData.Model.Services;

namespace RunningData.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);
            services.AddIoC(Configuration)
            .AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                    });
            })
            .AddMvc(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionHandler));
                //options.Filters.Add(typeof(AuthorizeUserTokenAttribute)); // Can't register the filter here since it uses DI
            })
            .AddControllersAsServices();

            services.AddApiVersioning(o =>
            {
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddScoped<AuthorizeUserTokenAttribute>(); // <-- Retrives the instance of the filter from DI
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }

    static class Extensions
    {
        public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IExceptionService, ExceptionService>();
            services.AddTransient<IService<FuelDataDto>, FuelDataService>();
            services.AddTransient<IFuelDataRepository, FuelDataRepository>();
            services.AddTransient<IDataConnection, SqlDataConnection>();

            return services;
        }

        public static IServiceCollection ModelMapping(this IServiceCollection services, IConfiguration configuration)
        {
            Mapper.Initialize(cfg =>
            {
                
            });
            return services;
        }
    }
}
