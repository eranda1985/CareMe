using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analytics.Api.Exceptions;
using Analytics.Core;
using Analytics.Model.Dto;
using Analytics.Model.Models;
using Analytics.Model.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Analytics.Api
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration config)
		{
			Configuration = config;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<AppSettings>(Configuration)
			.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", builder =>
				{
					builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
				});
			})
			.AddModelMapping()
			.AddMvc(options =>
			{
				options.Filters.Add<GlobalExceptionHandler>();
			})
			.AddControllersAsServices();

			services.AddApiVersioning(o =>
			{
				o.ApiVersionReader = new HeaderApiVersionReader("api-version");
			});
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

	static class ServiceExtensions
	{
		public static IServiceCollection AddModelMapping(this IServiceCollection services)
		{
			Mapper.Initialize(config =>
			{
				config.CreateMap<UserDataModel, UserDataDto>()
				.ForMember(dst => dst.Secret, opt => opt.MapFrom(src => src.SecretKey))
				.ForMember(dst => dst.Username, opt => opt.MapFrom(src => src.UserName));
			});

			return services;
		}

		public static IServiceCollection AddIoC(this IServiceCollection services)
		{
			// services 
			services.AddTransient<IService<UserDataDto>, UserDataService>();

			// respositories


			return services;
		}
	}
}
