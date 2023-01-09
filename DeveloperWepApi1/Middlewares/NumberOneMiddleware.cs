using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeveloperWepApi1.Middlewares
{
    public class NumberOneMiddleware
    {
        private readonly RequestDelegate _next;

        public NumberOneMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("1");
            await _next.Invoke(httpContext);
            Console.WriteLine("1");
           
        }
    }
}