using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardAPI.Context;
using DashboardAPI.Repositories;
using DashboardAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DashboardAPI.Service;
using DashboardAPI.Service.Interfaces;
using DashboardAPI.Extenstions;
using DashboardAPI.Entities;
using DashboardAPI.Filters;
using JustLogin.TokenProvider;
using Microsoft.OpenApi.Models;

namespace DashboardAPI
{
	public class Startup : BaseStartup
	{
		/// Construct and extend for Startup
		public Startup(IConfiguration configuration) : base(configuration)
		{

		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public override void ConfigureServices(IServiceCollection services)
		{
			base.ConfigureServices(services);
			// Add filters
			services.AddScoped<ValidateModelAttribute>();

			#region app extensions
			// Get value from env for credential/secret
			var templateDBConn = Environment.GetEnvironmentVariable("DBConnection") != null ? Environment.GetEnvironmentVariable("DBConnection") : Configuration.GetConnectionString("DBConnection");
			DBConnectionSetting dbSetting = new DBConnectionSetting();
			dbSetting.MembershipDBConn = templateDBConn;
			services.ConfigureDBContext(dbSetting);
			services.ConfigureServices();
			services.ConfigureSettings(Configuration);
			services.ConfigureCors();
			#endregion

			services.AddAutoMapper(typeof(Startup));
			services.AddControllers();

			// TODO: Add EventEmitterComponent
			// ConfigurationManager.AppSettings["EventControllerTrafficCop"] = Configuration.GetSection("ExternalConfig:EventTrafficCopARN").Value;

			services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dashboard API", Version = "v1" });
            });
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			var basePath = Environment.GetEnvironmentVariable("BasePath");
            if (string.IsNullOrEmpty(basePath))
            {
                throw new Exception("Base Path Environment Variable not set");
            }
            app.UsePathBase($"/{basePath}");

			#region app extensions
			app.UseCors("CorsPolicy");
			// app.UseCors(options => options.WithOrigins("http://localhost:4200").WithMethods("PUT", "DELETE", "GET", "POST").WithHeaders("Content-Type"));
			app.UseExceptionMiddleware();
			#endregion

			// Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/{basePath}/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
