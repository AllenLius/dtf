using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtf.Endpoint.Win
{
    using Dtf.Core;

    internal class WinValuePattern : IValuePattern
    {
        private IWinAutomation m_winAutomation;
        private Expression m_ui;

        public WinValuePattern(IWinAutomation winAutomation, Expression ui) 
        {
            m_winAutomation = winAutomation;
            m_ui = ui;
        }

        public void SetValue(string value)
        {
            m_winAutomation.ValuePattern_SetValue(m_ui, value);
        }
    }
}
