using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dta.Endpoint.Win
{
    using System.Diagnostics;
    using Dta.Core;
    
    public class WinApp : IApp
    {
        private IWinAutomation m_winAutomation;
        private string m_path;
        private string m_arguments;
        private string m_workingDir;
        private int m_procId;

        public WinApp(IWinAutomation winAutomation, string path, string arguments, string workingDirectory)
        {
            m_winAutomation = winAutomation;
            m_path = path;
            m_arguments = arguments;
            m_workingDir = workingDirectory;
        }

        public void Launch()
        {
            m_procId = m_winAutomation.Process_Start(m_path, m_arguments, m_workingDir);
        }

        public void Close()
        {
            m_winAutomation.Process_Close(m_procId);
        }
    }
}
