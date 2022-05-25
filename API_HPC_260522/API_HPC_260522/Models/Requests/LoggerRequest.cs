using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_HPC_260522.Models.Requests
{
    public class LoggerRequest
    {
        public string CorrelacionId { get; set; }
        public string Request { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public int EstatusCode { get; set; }
        public string Response { get; set; }
        public string ErrorMessage { get; set; }
    }
}
