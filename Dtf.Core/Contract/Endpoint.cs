using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtf.Core
{
    using System.Reflection;

    public interface IEndpoint
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Factory</returns>
        T QueryInterface<T>();
    }
}
