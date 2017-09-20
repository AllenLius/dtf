
using System.Diagnostics;
namespace Dtf.Core
{
    /// <summary>
    /// Process name to Process Id
    /// </summary>
    [HandlerName("ProcessIdResourceHandler")]
    public class ProcessIdResourceHandler : IResourceHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceKey">Process Name</param>
        /// <returns>string of Process Id</returns>
        public object GetObject(string resourceKey)
        {
            return Process.GetProcessesByName(resourceKey)[0].Id;
        }
    }
}
