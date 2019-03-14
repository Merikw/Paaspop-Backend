using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PaaspopService.Common.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(new ExceptionObject {Message = ex.Message}.ToString());
            }
            catch (Exception ex)
            {
                await context.Response.WriteAsync(new ExceptionObject { Message = ex.Message }.ToString());
            }
        }
    }
}