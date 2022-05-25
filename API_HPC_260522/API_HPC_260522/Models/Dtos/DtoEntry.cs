using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_HPC_260522.Models.Dtos
{
    public class DtoEntry
    {
        [JsonPropertyName("API")]
        public string API { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Auth")]
        public string Auth { get; set; }

        [JsonPropertyName("HTTPS")]
        public bool HTTPS { get; set; }

        [JsonPropertyName("Cors")]
        public string Cors { get; set; }

        [JsonPropertyName("Link")]
        public string Link { get; set; }

        [JsonPropertyName("Category")]
        public string Category { get; set; }
    }
}
