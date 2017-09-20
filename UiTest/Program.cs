using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Dtf.Core;
using Dtf.Endpoint.Win;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CalcTest
{
    class Program
    {
        static void DoSample()
        {
            while(true)
                Console.WriteLine("sample");
        }

        static void Main(string[] args)
        {
            var proc = Process.GetProcessesByName("Calculator");
            if (proc.Length > 0)
            {
                proc[0].Kill();
            }
            //Endpoint remote1 = RemoteWinEndpoint.Create("http://test-pc:8080");
            Endpoint local = LocalWinEndpoint.Instance;
            IAppFactory appFactory = local.QueryInterface<IAppFactory>();

            IApp app = appFactory.Create(@"Calc.exe");

            app.Launch();
            Thread.Sleep(1000);

            CallbackResourceHandler.Callback = (s) =>
            {
                return "1845951237";
            };

            var localCalc = new CalcUi(local);
            localCalc.One.Invoke();
            localCalc.Plus.Invoke();
            localCalc.Two.Invoke();
            localCalc.Equals.Invoke();
            Debug.Assert(localCalc.Result.GetProperty("Name").Replace("Display is ", string.Empty).Trim().Equals("3"));
        } 
    }
}
