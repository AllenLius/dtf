using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dtf.Core;

namespace Sandbox 
{
    public static class WinActivities
    {
        static IApp g_app = null;
        static IInvokePattern g_patterns = null;

        static WinActivities()
        {
            g_app = Proxy.WSHttp.GetOrCreate<IApp>(Server);
            g_patterns = Proxy.WSHttp.GetOrCreate<IInvokePattern>(Server);
        }

        public static IApp App
        {
            get
            {
                return g_app;
            }
        }

        public static IInvokePattern InvokePattern
        {
            get
            {
                return g_patterns;
            }
        }

        public static string Server
        {
            get
            {
                return "http://localhost/local";
            }
        }
    }
}
