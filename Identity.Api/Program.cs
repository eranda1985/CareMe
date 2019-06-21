using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Identity.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("hosting.json", optional: true)
				.Build();

			BuildWebHost(args, config).Run();
		}

		public static IWebHost BuildWebHost(string[] args, IConfiguration config) =>
			WebHost.CreateDefaultBuilder(args)
			.UseKestrel()
			.UseConfiguration(config)
			.UseStartup<Startup>()
			.ConfigureLogging((hostingContext, logging) =>
			{
				logging.AddLog4Net();
				logging.SetMinimumLevel(LogLevel.Debug);
				})
			.Build();
	}
}
