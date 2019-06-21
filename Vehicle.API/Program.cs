using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Vehicle.API
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
			.UseStartup<Startup>()
			.ConfigureLogging((hostingCtx, loggerBuilder) =>
			{
				loggerBuilder.AddLog4Net();
				loggerBuilder.SetMinimumLevel(LogLevel.Debug);
			});
	}
}
