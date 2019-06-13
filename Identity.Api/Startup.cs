using AutoMapper;
using CareMe.IntegrationService;
using CareMe.RabbitMQIntegrationService;
using Identity.Api.ActionFilters;
using Identity.Api.Exceptions;
using Identity.Core;
using Identity.Model.DataConnections;
using Identity.Model.Dto;
using Identity.Model.Factory;
using Identity.Model.Models;
using Identity.Model.Repositories;
using Identity.Model.Repositories.Interfaces;
using Identity.Model.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Identity.Api
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
            services.Configure<AppSettings>(Configuration)
                    .AddIoC(Configuration)
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
                        options.Filters.Add(typeof(ValidateModelAttribute));
                    }).AddControllersAsServices();

            services.AddApiVersioning(o =>
            {
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFac)
        {
            loggerFac.AddLog4Net();
            app.UseMvc();
        }
    }

    static class Extensions
    {
        public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IVersionRepository, VersionRepository>();
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();

            services.AddTransient<IService<UserDto>, UserService>();
            services.AddTransient<IService<EmailDto>, EmailService>();
            services.AddTransient<IService<VersionDto>, VersionService>();
            services.AddTransient<IClientFactory, ClientFactory>();
            services.AddTransient<IExceptionService, ExceptionService>();

            services.AddTransient<IDataConnection, SqlDataConnection>();

            services.AddSingleton<IServiceBus, RabbitMQServiceBus>(sp=> 
						{
							return new RabbitMQServiceBus();
						});
            return services;
        }

        public static IServiceCollection ModelMapping(this IServiceCollection services, IConfiguration configuration)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserModel, UserDto>();
                cfg.CreateMap<AppVersionModel, VersionDto>();
                cfg.CreateMap<UserProfileModel, UserProfileDto>()
                .ForMember(dest => dest.First, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Last, opt => opt.MapFrom(src => src.LastName));
            });
            return services;
        }
    }
}
