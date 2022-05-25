using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_HPC_260522.Common.Utils
{
    public static class Util
    {
        public static bool HasValue(this string value) => !string.IsNullOrEmpty(value);
    }
}
