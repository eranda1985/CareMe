using AutoMapper;
using CareMe.IntegrationService;
using CareMe.RabbitMQIntegrationService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using Vehicle.Api.ActionFilters;
using Vehicle.Api.Exceptions;
using Vehicle.Api.IntegrationEventHandlers;
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

			services.AddScoped<AuthorizeUserTokenAttribute>(); // <-- Retrieves the instance of the filter from DI
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddLog4Net(string.Format(@"{0}/log4net.config", AssemblyDirectory));

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			var eventBus = app.ApplicationServices.GetRequiredService<IServiceBus>();
			eventBus.Subscribe<IdentityUserAddedEvent, IdentityUserAddEventHandler>("UserAddedVehicle");

			app.UseMvc();
		}
	}

	internal static class Extensions
	{
		public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<IExceptionService, ExceptionService>();
			services.AddTransient<IService<VehicleDataDto>, VehicleDataService>();
			services.AddTransient<IService<UserDataDto>, UserdataService>();

			services.AddTransient<IVehicleDataRepository, VehicleDataRepository>();
			services.AddTransient<IUserDataRepository, UserDataRepository>();
			services.AddTransient<IVehicleUserDataRepository, VehicleUserDataRepository>();
			services.AddTransient<IVehicleTypeRepository, VehicleTypeRepository>();
			services.AddTransient<IVehicleBrandRepository, VehicleBrandRepository>();
			services.AddTransient<IVehicleModelRepository, VehicleModelRepository>();


			services.AddTransient<IDataConnection, SqlDataConnection>();
			services.AddTransient<IdentityUserAddEventHandler>();

			services.AddSingleton<ISubscriptionManager, VehicleSubscriptionManager>();
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
				cfg.CreateMap<VehicleDataModel, VehicleDataDto>();
				cfg.CreateMap<VehicleDataDto, VehicleDataModel>();
				cfg.CreateMap<VehicleTypeModel, VehicleTypeDto>();
				cfg.CreateMap<VehicleBrandModel, VehicleBrandDto>();
				cfg.CreateMap<VehicleModelDataModel, VehicleModelDto>();


				cfg.CreateMap<UserDataDto, UserDataModel>()
					.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
					.ForMember(dest => dest.SecretKey, opt => opt.MapFrom(src => src.Secret));
			});
			return services;
		}
	}
}
