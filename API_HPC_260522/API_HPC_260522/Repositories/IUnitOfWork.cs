using API_HPC_260522.Repositories.Database;
using API_HPC_260522.Repositories.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_HPC_260522.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        IEntriesRepository EntriesRepository { get; }
        ILoggerRepository LoggerRepository { get; }

        void BeginTransaction();
        void CommitChanges();
        void RollbackChanges();
    }
}
