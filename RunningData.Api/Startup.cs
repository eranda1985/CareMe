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
using RunningData.Api.Exceptions;
using RunningData.Core;
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
                    })
                .AddControllersAsServices();
            services.AddApiVersioning(o =>
            {
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
            loggerFactory.AddLog4Net();
        }
    }

    static class Extensions
    {
        public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IExceptionService, ExceptionService>();
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
