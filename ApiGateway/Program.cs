using System;
using System.IO;
using System.Reflection;
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
				.AddJsonFile(string.Format(@"{0}/hosting.json", AssemblyDirectory), optional: true)
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
