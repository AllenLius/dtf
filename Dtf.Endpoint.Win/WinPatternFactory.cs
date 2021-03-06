﻿namespace Dtf.Endpoint.Win
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dtf.Core;

    internal class WinPatternFactory : IPatternFactory
    {
        private IWinAutomation m_winAutomation;

        public WinPatternFactory(IWinAutomation winAutomation)
        {
            m_winAutomation = winAutomation;
        }

        public T Create<T>(Expression ui)
        {
            object instance = null;
            if (typeof(T) == typeof(IInvokePattern))
            {
                instance = new WinInvokePattern(m_winAutomation, ui);
            }
            else if (typeof(T) == typeof(IMousePattern))
            {
                instance = new WinMousePattern(m_winAutomation, ui);
            }
            else if (typeof(T) == typeof(IValuePattern))
            {
                instance = new WinValuePattern(m_winAutomation, ui);
            }
            return (T)instance;
        }
    }
}