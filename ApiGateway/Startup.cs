using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Reflection;
using System.IO;

namespace ApiGateway
{
  public class Startup
  {
    private IConfiguration configuration;

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

		public Startup(IHostingEnvironment env)
    {
        var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddJsonFile(string.Format(@"{0}/ocelot.json", AssemblyDirectory))
            .AddEnvironmentVariables();

      configuration = configurationBuilder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddOcelot(configuration); // <- Ocelot integration
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseOcelot().Wait(); // <- Use Ocelot 
    }
  }
}
