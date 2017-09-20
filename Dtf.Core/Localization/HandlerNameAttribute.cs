using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dtf.Core
{
    public class HandlerNameAttribute : Attribute
    {
        public HandlerNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
