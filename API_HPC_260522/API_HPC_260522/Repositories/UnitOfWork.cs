using API_HPC_260522.Repositories.External;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_HPC_260522.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IEntriesRepository _entriesRepository = null;
        private readonly IConfiguration _configuration;


        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEntriesRepository EntriesRepository => _entriesRepository ??= new EntriesRepository(_configuration);
    }
}
