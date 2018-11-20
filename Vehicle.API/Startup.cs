using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Vehicle.Api.Exceptions;
using Vehicle.Core;
using Vehicle.Model.DataConnections;
using Vehicle.Model.Dto;
using Vehicle.Model.Models;
using Vehicle.Model.Repositories;
using Vehicle.Model.Repositories.Interfaces;
using Vehicle.Model.Services;

namespace Vehicle.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);
            services.AddIoC(Configuration)
                .ModelMapping(Configuration)
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
                        })
                        .AddControllersAsServices();

            services.AddApiVersioning(o =>
            {
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            //services.AddScoped<AuthorizeUserTokenAttribute>(); // <-- Retrieves the instance of the filter from DI
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            services.AddTransient<IService<VehicleDataDto>, VehicleDataService>();
            services.AddTransient<IVehicleDataRepository, VehicleDataRepository>();
            services.AddTransient<IDataConnection, SqlDataConnection>();

            return services;
        }

        public static IServiceCollection ModelMapping(this IServiceCollection services, IConfiguration configuration)
        {
            Mapper.Initialize(cfg=> 
            {
                cfg.CreateMap<VehicleDataModel, VehicleDataDto>();
            });
            return services;
        }
    }
}
