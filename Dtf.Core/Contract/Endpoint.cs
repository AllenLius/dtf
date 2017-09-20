using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtf.Core
{
    using System.Reflection;

    public abstract class Endpoint
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Factory</returns>
        public abstract T QueryInterface<T>();
    }
}
