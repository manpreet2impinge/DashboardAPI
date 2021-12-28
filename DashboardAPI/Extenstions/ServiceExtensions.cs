using DashboardAPI.Context;
using DashboardAPI.Entities;
using DashboardAPI.Repositories;
using DashboardAPI.Repositories.Interfaces;
using DashboardAPI.Service;
using DashboardAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardAPI.Extenstions
{
	/// App Startup Configuration
	public static class ServiceExtensions
	{
		/// Cross Origin Config
		public static void ConfigureCors(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
			});
		}

		/// DB Configuration
		public static void ConfigureDBContext(this IServiceCollection services, DBConnectionSetting dbSetting)
		{
			services.AddDbContext<ExpressDbContext>(opts => opts.UseSqlServer(dbSetting.MembershipDBConn));
		}

		/// Dependency Injection(DI)
		public static void ConfigureServices(this IServiceCollection services)
		{
			// Add repositories
			services.AddScoped<ICompanyLinkRepository, CompanyLinkRepository>();
			services.AddScoped<IHappinessRepository, HappinessRepository>();

			// Add App Services
			services.AddScoped<IHappinessService, HappinessService>();
			services.AddScoped<ICompanyLinkService, CompanyLinkService>();
			
		}

		/// DI for Configuration Settings
		public static void ConfigureSettings(this IServiceCollection services, IConfiguration config)
		{
			//services.Configure<PasswordSetting>(config.GetSection("PasswordSetting"));
		}
	}
}
