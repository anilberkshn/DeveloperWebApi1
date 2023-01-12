using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using DeveloperWepApi1.Model.ErrorModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DeveloperWepApi1.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (DeveloperNotFoundException e)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = e.StatusCode;
                await httpContext.Response.WriteAsync(e.ToString());
               _logger.LogInformation(e, "bilinen bir hata oluştu");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

      }
}