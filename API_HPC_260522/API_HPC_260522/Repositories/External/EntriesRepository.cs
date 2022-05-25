using API_HPC_260522.Common.Utils;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_HPC_260522.Repositories.External
{
    public class EntriesRepository : IEntriesRepository
    {
        private readonly string _baseUrl;
        public EntriesRepository(IConfiguration configuration)
        {
            _baseUrl = configuration.GetSection("EntriesAPI").Value;
        }

        public TResponse GetCategories<TResponse>()
        {
            TResponse response = default(TResponse);
            ExceptionFlurl.GlobalTryCatch(() =>
            {
                response = _baseUrl
               .AppendPathSegment("categories")
               .GetJsonAsync<TResponse>().Result;
            });
            return response;
        }

        public TResponse GetEntries<TResponse>()
        {
            TResponse response = default(TResponse);
            ExceptionFlurl.GlobalTryCatch(() =>
            {
                response = _baseUrl
               .AppendPathSegment("entries")
               .GetJsonAsync<TResponse>().Result;
            });
            return response;
        }

        public TResponse GetEntries<TResponse, TRequest>(TRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
