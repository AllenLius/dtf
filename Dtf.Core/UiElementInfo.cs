using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Dtf.Core
{
    internal class  UiElementInfo
    {
        string m_name;
        Expression m_condition;
        Type[] m_patterns;
        List<UiElementInfo> m_children = new List<UiElementInfo>();

        public UiElementInfo(string name, Type[] patterns, Expression condition)
        {
            m_name = name;
            m_patterns = patterns;
            m_condition = condition;
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public Expression Condition
        {
            get { return m_condition; }
            set { m_condition = value; }
        }

        public Type[] Patterns
        {
            get { return m_patterns; }
            set { m_patterns = value; }
        }

        public IList<UiElementInfo> Children
        {
            get
            {
                return m_children;
            }
        }
    }
}
