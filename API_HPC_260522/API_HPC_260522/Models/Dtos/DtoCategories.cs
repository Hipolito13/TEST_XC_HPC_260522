using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_HPC_260522.Models.Dtos
{
    public class DtoCategories:DtoBase
    {
        [JsonPropertyName("categories")]
        public List<string> Categories { get; set; }
    }
}
