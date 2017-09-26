using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtf.Core
{
    public interface IResourceManager
    {
        string GetObject(string handlerType, string resourceKey);
    }
}
