﻿namespace Dta.Endpoint.Win
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dta.Core;

    internal class WinInvokePattern : IInvokePattern
    {
        private IWinAutomation m_winAutomation;
        private Expression m_ui;

        public WinInvokePattern(IWinAutomation winAutomation, Expression ui)
        {
            m_winAutomation = winAutomation;
            m_ui = ui;
        }

        public void Invoke()
        {
            m_winAutomation.InvokePattern_Invoke(m_ui);
            //var target = UiaUiObject.Root.FindFirst(ui) as UiaUiObject;
            //var invoke = target.Current.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            //invoke.Invoke();
        }
    }
}