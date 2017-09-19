using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dta.Core
{
    internal static class Validate
    {
        internal static void ArgumentNotNull(object argument, string argumentName)
        {
            if (object.ReferenceEquals(argument, null))
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
