using DashboardAPI.Common.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DashboardAPI.Middlewares
{
    /// <summary>
    /// Global Exception Middleware
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <summary>
        /// Constructor of global exception middleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        /// <summary>
        /// Execute method when a exception occurs
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"InvokeAsync went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ErrorViewModel error = new ErrorViewModel
            {
                code = context.Response.StatusCode,
                message = exception.Message
            };

            _logger.LogError($"Error from custom middleware :{exception.Message}");
            //var resp = ResponseHelper.GetReponseForError(exception, context.Response.StatusCode);
            var jsonResponse = JsonConvert.SerializeObject(error);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
