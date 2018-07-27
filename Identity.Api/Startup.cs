using Identity.Api.Exceptions;
using Identity.Core;
using Identity.Model.Dto;
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
            }).AddControllersAsServices(); // <- TODO: Add model state validation.

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
            services.AddTransient<IService<UserDto>, UserService>();
            services.AddTransient<IExceptionService, ExceptionService>();
            return services;
        }
    }
}
