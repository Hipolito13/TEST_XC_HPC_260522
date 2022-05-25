using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_HPC_260522.Models.Responses
{
    public class CategoriesResponse:BaseResponse
    {
        [JsonPropertyName("categories")]
        public List<CategoryResponse> Categories { get; set; }
    }

    public class CategoryResponse
    {
        [JsonPropertyName("CategoryName")]
        public string CategoryName { get; set; }
    }

}
