using API_HPC_260522.Models;
using API_HPC_260522.Repositories.Database;
using API_HPC_260522.Repositories.External;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API_HPC_260522.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IEntriesRepository _entriesRepository = null;
        private readonly IConfiguration _configuration;
        private ILoggerRepository _loggerRepository ;
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction = null;

        public UnitOfWork(IConfiguration configuration, IDbConnection connection)
        {
            _configuration = configuration;
            _connection = connection;
        }

        public IEntriesRepository EntriesRepository => _entriesRepository ??= new EntriesRepository(_configuration);

        public ILoggerRepository LoggerRepository => _loggerRepository ??= new LoggerRepository(_connection);

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void CommitChanges()
        {
            _transaction.Commit();
        }

        public void RollbackChanges()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();

            _transaction = null;
        }
    }
}
