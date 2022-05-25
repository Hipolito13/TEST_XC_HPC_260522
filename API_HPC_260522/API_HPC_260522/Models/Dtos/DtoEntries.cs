using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_HPC_260522.Models.Dtos
{
    public class DtoEntries: DtoBase
    {
        
        [JsonPropertyName("entries")]
        public List<DtoEntry> Entries { get; set; }
    }
}
