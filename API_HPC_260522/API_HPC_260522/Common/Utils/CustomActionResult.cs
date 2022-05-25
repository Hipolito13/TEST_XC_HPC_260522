using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API_HPC_260522.Common.Utils
{
    public class CustomActionResult<T> where T : class
    {
        public static IActionResult CreateResult(T response, HttpStatusCode statusCode)
        {
            return (statusCode) switch
            {
                HttpStatusCode.OK => new OkObjectResult(response),
                HttpStatusCode.BadRequest => new BadRequestObjectResult(response),
                HttpStatusCode.NotFound => new NotFoundObjectResult(response),
                HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(response),
                HttpStatusCode.InternalServerError => new ObjectResult(response) { StatusCode = (int)HttpStatusCode.InternalServerError },
                _ => new ObjectResult(response) { StatusCode = (int)HttpStatusCode.InternalServerError },
            };
        }
    }
}
