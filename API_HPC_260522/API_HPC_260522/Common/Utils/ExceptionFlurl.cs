using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_HPC_260522.Common.Utils
{
    public static class ExceptionFlurl
    {
        public static void GlobalTryCatch(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                if (ex.InnerException is FlurlHttpException fex)
                {
                    throw new Exception(fex.GetResponseStringAsync().Result);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
