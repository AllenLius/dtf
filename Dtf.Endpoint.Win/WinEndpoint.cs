namespace Dtf.Endpoint.Win
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dtf.Core;

    public abstract class WinEndpoint : Endpoint
    {
        private List<WeakReference> m_objectRefs = new List<WeakReference>();
        private IWinAutomation m_winAutomation;
        private Dictionary<Type, object> m_objectMap = new Dictionary<Type, object>();

        public WinEndpoint(IWinAutomation winAutomation)
        {
            m_winAutomation = winAutomation;
            m_objectMap.Add(typeof(IAppFactory), new WinAppFactory(winAutomation));
            m_objectMap.Add(typeof(IPatternFactory), new WinPatternFactory(winAutomation));
            m_objectMap.Add(typeof(IUiInspectorFactory), new WinUiInspectorFactory(winAutomation));
        }

        public override T QueryInterface<T>()
        {
            object obj;
            m_objectMap.TryGetValue(typeof(T), out obj);
            return (T)obj;
        }

        protected IWinAutomation WinAutomation
        {
            get
            {
                return m_winAutomation;
            }
        }
    }
}
