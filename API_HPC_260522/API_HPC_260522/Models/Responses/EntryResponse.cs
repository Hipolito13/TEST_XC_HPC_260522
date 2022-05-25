using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_HPC_260522.Models.Responses
{
    public class EntryResponse
    {
        [JsonPropertyName("Name")]
        public string ApiName { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("AuthType")]
        public string AuthType { get; set; }

        [JsonPropertyName("IsHttp")]
        public bool IsHttp { get; set; }

        [JsonPropertyName("Cors")]
        public string Cors { get; set; }

        [JsonPropertyName("Link")]
        public string Link { get; set; }

        [JsonPropertyName("CategoryName")]
        public string CategoryName { get; set; }
    }
}
