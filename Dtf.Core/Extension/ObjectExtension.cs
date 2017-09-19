using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dta.Core
{
    public static class ObjectExtension
    {
        public static string GetOverrideToString(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            bool hasOverridedToString = !obj.GetType().GetMethod("ToString", new Type[] { }).DeclaringType.Equals(typeof(object));
            return hasOverridedToString ? obj.ToString() : null;
        }
    }
}
