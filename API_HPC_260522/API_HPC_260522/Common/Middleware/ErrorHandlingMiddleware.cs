using API_HPC_260522.Common.Utils;
using API_HPC_260522.Models.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API_HPC_260522.Common.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //var message = exception switch
            //{
            //    AccessViolationException => "Access violation error from the custom middleware",
            //    _ => "Internal Server Error from the custom middleware."
            //};
            await context.Response.WriteAsync(new BaseResponse { 
               IsValid = false,
               Errors = new List<Error>
               {
                   new Error
                   {
                       Text =exception.Message,
                       Type = HttpStatusCode.InternalServerError.ToString(),
                       Code = (int)HttpStatusCode.InternalServerError
                   }
               }
            }.ToJsonString());
        }
    }
}
