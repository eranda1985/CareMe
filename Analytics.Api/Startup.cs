﻿using Analytics.Api.ActionFilters;
using Analytics.Api.Exceptions;
using Analytics.Api.IntegrationEventHandlers;
using Analytics.Core;
using Analytics.Model.DataConnections;
using Analytics.Model.Dto;
using Analytics.Model.Models;
using Analytics.Model.Repositories;
using Analytics.Model.Repositories.Interfaces;
using Analytics.Model.Services;
using AutoMapper;
using CareMe.IntegrationService;
using CareMe.RabbitMQIntegrationService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
			.AddIoC()
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

			services.AddScoped<AuthorizeUserTokenAttribute>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddLog4Net();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			var bus = app.ApplicationServices.GetRequiredService<IServiceBus>();
			bus.Subscribe<IdentityUserAddedEvent, IdentityUserAddedEventHandler>("UserAddedAnalytics");
			bus.Subscribe<NewVehicleAddedEvent, NewVehicleAddedEventHandler>("VehicleAddedAnalytics");

			app.UseMvc();
		}
	}

	internal static class ServiceExtensions
	{
		public static IServiceCollection AddModelMapping(this IServiceCollection services)
		{
			Mapper.Initialize(config =>
			{
				config.CreateMap<UserDataModel, UserDataDto>()
				.ForMember(dst => dst.Secret, opt => opt.MapFrom(src => src.SecretKey))
				.ForMember(dst => dst.Username, opt => opt.MapFrom(src => src.UserName));

				config.CreateMap<UserDataDto, UserDataModel>()
				.ForMember(dst => dst.SecretKey, opt => opt.MapFrom(src => src.Secret))
				.ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.Username));
			});

			return services;
		}

		public static IServiceCollection AddIoC(this IServiceCollection services)
		{
			// services 
			services.AddTransient<IService<UserDataDto>, UserDataService>();
			services.AddTransient<IExceptionService, ExceptionService>();


			// respositories
			services.AddTransient<IUserDataRepository<UserDataModel>, UserDataRepository>();
			services.AddTransient<IDataConnection, SqlDataConnection>();

			// MQ integration
			services.AddTransient<IdentityUserAddedEventHandler>();
			services.AddTransient<NewVehicleAddedEventHandler>();

			services.AddSingleton<ISubscriptionManager, AnalyticsSubscriptionManager>(sp =>
			{
				var userAddedHandler = sp.GetRequiredService<IdentityUserAddedEventHandler>();
				var vehicleAddedHandler = sp.GetRequiredService<NewVehicleAddedEventHandler>();

				return new AnalyticsSubscriptionManager(userAddedHandler, vehicleAddedHandler);
			});

			services.AddSingleton<IServiceBus, RabbitMQServiceBus>(sp =>
			{
				var manager = sp.GetRequiredService<ISubscriptionManager>();
				return new RabbitMQServiceBus(manager);
			});

			return services;
		}
	}
}
