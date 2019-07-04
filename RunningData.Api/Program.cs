using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;

namespace RunningData.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile(string.Format(@"{0}/hosting.json", AssemblyDirectory), optional: true)
				.AddJsonFile(string.Format(@"{0}/appsettings.json", AssemblyDirectory))
				.Build();

			CreateWebHostBuilder(args, config).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfiguration config) =>
			WebHost.CreateDefaultBuilder(args)
			.UseKestrel()
			.UseConfiguration(config)
			.UseStartup<Startup>()
			.ConfigureLogging((hostinContext, logging) =>
			{
				logging.AddLog4Net(string.Format(@"{0}/log4net.config", AssemblyDirectory));
				logging.SetMinimumLevel(LogLevel.Debug);
			});

		public static string AssemblyDirectory
		{
			get
			{
				string codeBase = Assembly.GetExecutingAssembly().CodeBase;
				UriBuilder uri = new UriBuilder(codeBase);
				string path = Uri.UnescapeDataString(uri.Path);
				return Path.GetDirectoryName(path);
			}
		}
	}
}
