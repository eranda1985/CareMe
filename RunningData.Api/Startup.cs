using AutoMapper;
using CareMe.IntegrationService;
using CareMe.RabbitMQIntegrationService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RunningData.Api.ActionFilters;
using RunningData.Api.Exceptions;
using RunningData.Api.IntegrationEventHandlers;
using RunningData.Core;
using RunningData.Model.DataConnections;
using RunningData.Model.Dto;
using RunningData.Model.Models;
using RunningData.Model.Repositories;
using RunningData.Model.Repositories.Interfaces;
using RunningData.Model.Services;
using System;
using System.IO;
using System.Reflection;

namespace RunningData.Api
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public string AssemblyDirectory
		{
			get
			{
				string codeBase = Assembly.GetExecutingAssembly().CodeBase;
				UriBuilder uri = new UriBuilder(codeBase);
				string path = Uri.UnescapeDataString(uri.Path);
				return Path.GetDirectoryName(path);
			}
		}

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
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
				//options.Filters.Add(typeof(AuthorizeUserTokenAttribute)); // Can't register the filter here since it uses DI
			})
			.AddControllersAsServices();

			services.AddApiVersioning(o =>
			{
				o.ApiVersionReader = new HeaderApiVersionReader("api-version");
			});

			services.AddScoped<AuthorizeUserTokenAttribute>(); // <-- Retrieves the instance of the filter from DI
		}


		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddLog4Net(string.Format(@"{0}/log4net.config", AssemblyDirectory));

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			var eventBus = app.ApplicationServices.GetRequiredService<IServiceBus>();
			eventBus.Subscribe<IdentityUserAddedEvent, IdentityUserAddEventHandler>("UserAddedRunningData");

			app.UseMvc();
		}
	}

	internal static class Extensions
	{
		public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<IExceptionService, ExceptionService>();
			services.AddTransient<IService<FuelDataDto>, FuelDataService>();
			services.AddTransient<IService<UserDataDto>, UserDataService>();
			services.AddTransient<IFuelDataRepository<FuelDataModel>, FuelDataRepository>();
			services.AddTransient<IUserDataRepository<UserDataModel>, UserDataRepository>();
			services.AddTransient<IDataConnection, SqlDataConnection>();
			services.AddTransient<IdentityUserAddEventHandler>();

			services.AddSingleton<ISubscriptionManager, RunningDataSubscriptionManager>();
			services.AddSingleton<IServiceBus, RabbitMQServiceBus>(sp =>
			{
				var subsManager = sp.GetRequiredService<ISubscriptionManager>();
				return new RabbitMQServiceBus(subsManager);
			});

			return services;
		}

		public static IServiceCollection ModelMapping(this IServiceCollection services, IConfiguration configuration)
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<UserDataDto, UserDataModel>()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
				.ForMember(dest => dest.SecretKey, opt => opt.MapFrom(src => src.Secret));
			});
			return services;
		}
	}
}
