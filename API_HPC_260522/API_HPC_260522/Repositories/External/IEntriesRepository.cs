using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_HPC_260522.Repositories.External
{
    public interface IEntriesRepository
    {
        TResponse GetEntries<TResponse>();
        TResponse GetEntries<TResponse, TRequest>(TRequest request);
        TResponse GetCategories<TResponse>();
    }
}
