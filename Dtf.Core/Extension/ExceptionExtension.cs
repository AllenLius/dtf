using System;
using System.Text;

namespace Dta.Core
{
    public static class ExceptionExtension
    {
        public static string GetInfo(this Exception ex)
        {
            return GetExceptionText(ex);
        }

        public static string GetExceptionText(this Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("type    ={0}", ex.GetType().Name));
            sb.AppendLine(String.Format("message ={0}", ex.Message));
            sb.AppendLine(String.Format("Stack   ={0}", ex.StackTrace));
            if (ex.InnerException != null)
            {
                sb.AppendLine(GetExceptionText(ex.InnerException));
            }
            return sb.ToString();
        }
    }
}
