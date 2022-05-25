using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_HPC_260522.Models.Responses
{
    public class EntriesResponse:BaseResponse
    {
        [JsonPropertyName("Entries")]
        public List<EntryResponse> Entries { get; set; }
    }
}
