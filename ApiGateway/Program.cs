using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ApiGateway
{
  public class Program
  {
    public static void Main(string[] args)
    {
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("hosting.json", optional: true)
				.Build();

			CreateWebHostBuilder(args, config).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfiguration config) =>
        WebHost.CreateDefaultBuilder(args)
      .UseKestrel()
			.UseConfiguration(config)
      .UseIISIntegration()
      .UseContentRoot(Directory.GetCurrentDirectory())
      .UseStartup<Startup>();
  }
}
