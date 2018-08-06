using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
{
  public class Startup
  {
    private IConfiguration configuration;

    public Startup(IHostingEnvironment env)
    {
        var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddJsonFile("ocelot.json")
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
