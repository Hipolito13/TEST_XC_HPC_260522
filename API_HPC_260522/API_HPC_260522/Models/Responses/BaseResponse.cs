using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_HPC_260522.Models.Responses
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            IsValid = true;
            Errors = new List<Error>();
        }

        [JsonPropertyName("IsValid")]
        public bool IsValid { get; set; }
        [JsonPropertyName("Errors")]
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        [JsonPropertyName("Text")]
        public string Text { get; set; }
        [JsonPropertyName("Code")]
        public int Code { get; set; }
        [JsonPropertyName("Type")]
        public string Type { get; set; }
    }
}
