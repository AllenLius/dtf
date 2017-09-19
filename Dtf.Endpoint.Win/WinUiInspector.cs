namespace Dta.Endpoint.Win
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dta.Core;

    public class WinUiInspector : IUiInspector
    {
        private IWinAutomation m_winAutomation;
        private Expression m_ui;

        public WinUiInspector(IWinAutomation winAutomation, Expression ui)
        {
            m_winAutomation = winAutomation;
            m_ui = ui;
        }

        public bool Exists()
        {
            return Exists(TimeSpan.Zero);
        }

        public bool Exists(TimeSpan timeout)
        {
            return m_winAutomation.UiObject_Exists(m_ui, timeout);
        }

        public void Wait(TimeSpan timeout)
        {
            if (!Exists(timeout))
            {
                throw new Exception("UI not found!");
            }
        }

        public string GetProperty(string propertyName)
        {
            return m_winAutomation.UiObject_GetProperty(m_ui, propertyName);
        }

        public string[] GetProperties()
        {
            return m_winAutomation.UiObject_GetProperties(m_ui);
        }

        public string GetUi()
        {
            return m_winAutomation.UiObject_GetUi(m_ui);
        }

        public byte[] Capture()
        {
            throw new NotImplementedException();
        }
    }
}
