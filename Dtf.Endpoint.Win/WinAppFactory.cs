using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtf.Endpoint.Win
{
    using System.Diagnostics;
    using Dtf.Core;
    
    public class WinAppFactory : IAppFactory
    {
        private IWinAutomation m_winAutomation;

        public WinAppFactory(IWinAutomation winAutomation)
        {
            m_winAutomation = winAutomation;
        }

        public IApp Create(string path, string arguments, string workingDirectory)
        {
            WinApp app = new WinApp(m_winAutomation, path, arguments, workingDirectory);
            return app;
        }
    }
}
