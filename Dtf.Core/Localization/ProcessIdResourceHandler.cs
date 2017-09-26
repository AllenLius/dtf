
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
        public string GetObject(string resourceKey)
        {
            var processes = Process.GetProcessesByName(resourceKey);
            if (processes.Length == 0)
            {
                return null;
            }
            return processes[0].Id.ToString();
        }
    }
}
