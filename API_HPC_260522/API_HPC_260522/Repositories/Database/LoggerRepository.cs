using API_HPC_260522.Common.Utils;
using API_HPC_260522.Models.Requests;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API_HPC_260522.Repositories.Database
{
    public class LoggerRepository : ILoggerRepository
    {
        private readonly IDbConnection _connection;

        public LoggerRepository(IDbConnection connection) 
        {
            _connection = connection;
        }

        public void AddLogger(LoggerRequest logger)
        {
            _connection.QueryFirstOrDefault<object>(Constants.Logger_Insert, 
                new { 
                    CorrelationId = logger.CorrelacionId,
                    Method = logger.Method,
                    Url = logger.Url,
                    Request = logger.Request,
                    Response = logger.Response,
                    Code = logger.EstatusCode
                }
                ,commandType: CommandType.StoredProcedure);
        }

        public void UpdateLogger(LoggerRequest logger)
        {
            _connection.QueryFirstOrDefault<object>(Constants.Logger_Update,new 
            {
                CorrelationId = logger.CorrelacionId,
                ErrorMessage = logger.ErrorMessage,
                Code = logger.EstatusCode,
                Response = logger.Response
            }, commandType: CommandType.StoredProcedure);
        }
    }
}
