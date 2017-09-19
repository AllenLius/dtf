using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dta.Core
{
    internal class ResourceInfo
    {
        private string m_name;        
        private string m_handlerType;       
        private string m_resourceKey;

        public ResourceInfo(string name, string handlerType, string resourceKey)
        {
            m_name = name;
            m_handlerType = handlerType;
            m_resourceKey = resourceKey;
        }

        public string Name
        {
            get { return m_name; }
        }

        public string HandlerType
        {
            get { return m_handlerType; }
        }

        public string ResourceKey
        {
            get { return m_resourceKey; }
        }
    }
}
