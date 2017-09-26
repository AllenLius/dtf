namespace Dtf.Endpoint.Win
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dtf.Core;

    public class LocalWinEndpoint : WinEndpoint
    {
        private static readonly LocalWinEndpoint _instance;
        private IWinAutomation m_winAutomation;

        static LocalWinEndpoint()
        {
            _instance = new LocalWinEndpoint();
        }

        private LocalWinEndpoint() :
            base(new WinAutomation())
        {
        }

        public static LocalWinEndpoint Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
