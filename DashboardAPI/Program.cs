using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace DashboardAPI
{
	public class Program
	{
		/// Main method of Template api
		public static void Main(string[] args)
		{
			var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
			try
			{
				logger.Debug("init main");
				// Check to see if we can read existing environment variables
				var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
				if (string.IsNullOrEmpty(environment))
				{
					// Required for Elastic Beanstalk
					if (!SetElasticBeanstalkbConfig())
					{
						throw new ApplicationException();
					}
				}

				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				//NLog: catch setup errors
				logger.Error(ex, ex.Message);
			}
			finally
			{
				// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
				NLog.LogManager.Shutdown();
			}
		}

		/// Construct and Config for Hosting
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseIIS();
					webBuilder.UseStartup<Startup>();
					webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
					{
						var env = hostingContext.HostingEnvironment;
						var ConfigPath = Path.Combine(env.ContentRootPath, "Configs");

						config.AddJsonFile(Path.Combine(ConfigPath, "secret.json"), optional: true, reloadOnChange: true)
							.AddJsonFile(Path.Combine(ConfigPath, "appsettings.json"), optional: true, reloadOnChange: true)
							.AddJsonFile(Path.Combine(ConfigPath, "ConnectionString.json"), optional: true, reloadOnChange: true);

						if (env.IsEnvironment("Development"))
						{
							config.AddJsonFile(Path.Combine(ConfigPath, "secret.json"), optional: true, reloadOnChange: true)
								.AddJsonFile(Path.Combine(ConfigPath, $"appsettings.{env.EnvironmentName}.json"), optional: true, reloadOnChange: true)
								.AddJsonFile(Path.Combine(ConfigPath, $"ConnectionString.{env.EnvironmentName}.json"), optional: true, reloadOnChange: true);
						}

						config.AddEnvironmentVariables();
					});
					webBuilder.ConfigureLogging(logging =>
					{
						logging.ClearProviders();
						logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
					});
					webBuilder.UseNLog();
				});

		private static bool SetElasticBeanstalkbConfig()
		{
			var tempConfigBuilder = new ConfigurationBuilder();

			tempConfigBuilder.AddJsonFile(
				@"C:\Program Files\Amazon\ElasticBeanstalk\config\containerconfiguration",
				optional: true,
				reloadOnChange: true
			);

			var configuration = tempConfigBuilder.Build();

			var ebEnv =
				configuration.GetSection("iis:env")
					.GetChildren()
					.Select(pair => pair.Value.Split(new[] { '=' }, 2))
					.ToDictionary(keypair => keypair[0], keypair => keypair[1]);

			foreach (var keyVal in ebEnv)
			{
				Environment.SetEnvironmentVariable(keyVal.Key, keyVal.Value);
			}

			return ebEnv.Count > 0 ? true : false;
		}
	}
}
