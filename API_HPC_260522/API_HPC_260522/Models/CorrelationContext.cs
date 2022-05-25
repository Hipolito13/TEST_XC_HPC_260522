using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_HPC_260522.Models
{
    public class CorrelationContext
    {
        private string _correlationId;

        public string CorrelationId { get => _correlationId; }

        public CorrelationContext() => GenerateCorrelationId();

        private void GenerateCorrelationId() => _correlationId = Guid.NewGuid().ToString();
    }
}
