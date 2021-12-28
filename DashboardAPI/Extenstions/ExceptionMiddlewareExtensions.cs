using DashboardAPI.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardAPI.Extenstions
{
	/// <summary>
	/// Use Exception Middleware in Startup
	/// </summary>
	public static class ExceptionMiddlewareExtensions
	{
		/// <summary>
		/// Global exception middleware inject to startup
		/// </summary>
		/// <param name="app"></param>
		public static void UseExceptionMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionMiddleware>();
		}
	}
}
