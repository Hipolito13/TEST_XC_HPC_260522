using API_HPC_260522.Common.Utils;
using API_HPC_260522.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API_HPC_260522.Common.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthenticationAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "Api-Key";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
            {
                context.Result = GetActionResult();
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>("Api-key");


            if (!apiKey.Equals(potentialApiKey))
            {
                context.Result = GetActionResult();
                return;
            }

            await next();
        }

        private IActionResult GetActionResult()
        {
            return CustomActionResult<UnauthorizedResponse>.CreateResult(new UnauthorizedResponse
            {
                IsValid = false,
                Errors = new List<Error>
                  {
                      new Error
                      {
                          Code = (int)HttpStatusCode.Unauthorized,
                          Text = "Authorization failed",
                          Type = HttpStatusCode.Unauthorized.ToString()
                      }
                  }
            }, HttpStatusCode.Unauthorized);
        }
    }
}
