using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_HPC_260522.Common.Utils
{
    public static class Util
    {
        public static bool HasValue(this string value) => !string.IsNullOrEmpty(value);

        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false, bool ignoreNullValues = false)
        {
            var options = new JsonSerializerOptions();

            if (camelCase)
            {
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }
            if (indented)
            {
                options.WriteIndented = true;
            }
            if (ignoreNullValues)
            {
                options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            }
            return JsonSerializer.Serialize(obj, options);
        }

        public static T DeserializeJsonStringToObject<T>(this string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString)) return default(T);

            var objResult = JsonSerializer.Deserialize<T>(jsonString);

            return objResult;
        }
    }

    
}
