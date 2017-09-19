namespace Dta.Endpoint.Win
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dta.Core;

    public abstract class WinEndpoint : Endpoint
    {
        private List<WeakReference<object>> _objectRefs = new List<WeakReference<object>>();

        public override T QueryInterface<T>()
        {
            object instance = null;

            if (typeof(IAppFactory) == typeof(T))
            {
                instance = new WinAppFactory(WinAutomation);
            }
            else if (typeof(IPatternFactory) == typeof(T))
            {
                instance = new WinPatternFactory(WinAutomation);
            }
            else if (typeof(IUiInspectorFactory) == typeof(T))
            {
                instance = new WinUiInspectorFactory(WinAutomation);
            }
            _objectRefs.Add(new WeakReference<object>(WinAutomation));
            return (T)instance;
        }

        protected abstract IWinAutomation WinAutomation { get; }
    }
}
