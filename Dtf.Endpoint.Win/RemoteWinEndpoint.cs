namespace Dtf.Endpoint.Win
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dtf.Core;
    using System.ServiceModel;

    public class RemoteWinEndpoint : WinEndpoint
    {
        private RemoteWinEndpoint(IWinAutomation winAutomation)
            : base(winAutomation)
        {
        }

        public static RemoteWinEndpoint Create(string uri)
        {
            BasicHttpBinding b = new BasicHttpBinding();
            b.Security.Mode = BasicHttpSecurityMode.None;
            IWinAutomation winAutomation = new Proxy(b).GetOrCreate<IWinAutomation>(uri);
            return new RemoteWinEndpoint(winAutomation);
        }
    }
}
