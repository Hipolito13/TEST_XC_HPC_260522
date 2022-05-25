using API_HPC_260522.Common.Utils;
using API_HPC_260522.Models.Dtos;
using API_HPC_260522.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API_HPC_260522.Services
{
    public class BaseService
    {
        public List<Error> GetErrors(string text, HttpStatusCode code)
        {
            return new List<Error>
            {
                new Error
                {
                   Code = (int)code,
                   Text = text,
                   Type = code.ToString()
                }
            };
        }

        public bool IsExistEntries(DtoEntries entries)
        {
            return entries.Count > 0;
        }

        public IActionResult GetNotFoundEntries()
        {
            return CustomActionResult<EntriesResponse>.CreateResult(new EntriesResponse
            {
                Errors = GetErrors("No Entries data found", HttpStatusCode.NotFound),
                IsValid = false,
            }, HttpStatusCode.NotFound);
        }

        public IActionResult OnError<TResponse>(TResponse response, HttpStatusCode statusCode) where TResponse:class
        {
            return CustomActionResult<TResponse>.CreateResult(response, statusCode);
        }

        public IActionResult OnSuccess<TResponse>(TResponse response) where TResponse : class
        {
            return CustomActionResult<TResponse>.CreateResult(response, HttpStatusCode.OK);
        }
    }
}
