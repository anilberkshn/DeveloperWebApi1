using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeveloperWepApi1.Middlewares
{
    public class NumberTwoMiddleware
    {
        private readonly RequestDelegate _next;

        public NumberTwoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("2");
            await _next.Invoke(httpContext);
            Console.WriteLine("2");
            
        }
    }
}