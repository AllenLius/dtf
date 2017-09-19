namespace Dta.Endpoint.Win
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dta.Core;

    public class WinUiInspectorFactory : IUiInspectorFactory
    {
        private IWinAutomation m_winAutomation;

        public WinUiInspectorFactory(IWinAutomation winAutomation)
        {
            m_winAutomation = winAutomation;
        }

        public IUiInspector Create(Expression ui)
        {
            return new WinUiInspector(m_winAutomation, ui);
        }
    }
}
