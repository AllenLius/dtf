using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Dtf.Core;
using Dtf.Endpoint.Win;
using System.Threading.Tasks;
using System.Diagnostics;
using CalcTest;

namespace Sandbox
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
            Endpoint local = LocalWinEndpoint.Instance;
            Endpoint remote1 = RemoteWinEndpoint.Create("http://localhost/GLIU-L01/");
            
            // do UI automation on remote machine
            WinAppFactory remote1AppFactory = remote1.QueryInterface<IAppFactory>() as Dtf.Endpoint.Win.WinAppFactory;
            var m = remote1.QueryInterface<IUiInspectorFactory>().Create((Expression)"<Equals Property=\"AutomationId\">CalculatorResults</Equals>");
            //m.Exists();

            // launch app
            IAppFactory localAppFactory = local.QueryInterface<IAppFactory>();
            IApp app = localAppFactory.Create(@"Calc.exe");            
            app.Launch();
            Thread.Sleep(1000);

            CallbackResourceHandler.Callback = (s) =>
            {
                return "1845951237";
            };

            var localCalc = new CalcUi(local);
            localCalc.Ui.Calculator.One.Invoke();
            localCalc.Ui.Calculator.Plus.Invoke();
            localCalc.Ui.Calculator.Two.Invoke();
            localCalc.Ui.Calculator.Equals.Invoke();
            Debug.Assert(localCalc.Ui.Calculator.Result.GetProperty("Name").Replace("Display is ", string.Empty).Trim().Equals("3"));
            app.Close();
        } 
    }
}
