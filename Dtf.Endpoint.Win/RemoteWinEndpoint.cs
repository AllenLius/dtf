namespace Dtf.Endpoint.Win
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dtf.Core;

    public class RemoteWinEndpoint : WinEndpoint
    {
        private IWinAutomation m_winAutomation;

        private RemoteWinEndpoint(string uri)
        {
            m_winAutomation = Proxy.WSHttp.GetOrCreate<IWinAutomation>(uri);            
        }

        public static RemoteWinEndpoint Create(string uri)
        {
            return new RemoteWinEndpoint(uri);
        }

        protected override IWinAutomation WinAutomation
        {
            get
            {
                return m_winAutomation;
            }
        }
    }
}
