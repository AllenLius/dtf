using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dta.Core
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class PatternAttribute : Attribute
    {
        private string m_name;

        public PatternAttribute(string name)
        {
            m_name = name;
        }

        public string Name
        {
            get { return m_name; }
        }
    }
}
