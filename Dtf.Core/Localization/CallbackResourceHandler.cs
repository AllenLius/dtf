
using System;
using System.Diagnostics;
namespace Dtf.Core
{
    /// <summary>
    /// Process name to Process Id
    /// </summary>
    [HandlerName("CallbackResourceHandler")]
    public class CallbackResourceHandler : IResourceHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public string GetObject(string resourceKey)
        {
            if (Callback == null)
            {
                throw new Exception("Callback not set!");
            }
            return Callback(resourceKey);
        }

        public static Func<string, string> Callback { get; set; }
    }
}
