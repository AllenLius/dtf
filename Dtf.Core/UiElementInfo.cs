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
        UiElementInfo m_parent;
        string m_name;
        Expression m_condition;
        Type[] m_patterns;

        public UiElementInfo(UiElementInfo parent, string name, Type[] patterns, Expression condition)
        {
            m_parent = parent;
            m_name = name;
            m_patterns = patterns;
            m_condition = condition;
        }

        public string Name
        {
            get { return m_name; }
        }

        public Expression Condition
        {
            get { return m_condition; }
        }

        public UiElementInfo Parent
        {
            get { return m_parent; }
        }

        public Type[] Patterns
        {
            get { return m_patterns; }
        }
    }
}
