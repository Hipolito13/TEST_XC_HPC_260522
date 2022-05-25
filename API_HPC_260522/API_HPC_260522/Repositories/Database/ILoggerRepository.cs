using API_HPC_260522.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_HPC_260522.Repositories.Database
{
    public interface ILoggerRepository
    {
        void AddLogger(LoggerRequest logger);
        void UpdateLogger(LoggerRequest logger);
    }
}
