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
    public interface IUi
    {
        string Ui { get; set; }
    }
    public abstract class UiEl
    {
        public string Ui { get; set; }
    }
    public class UiEl1 : UiEl, IUi
    {

    }
    public static class Ext
    {
        public static void Capture<T>(this IUiInspector ui) where T : UiElement
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            UiEl1 u = new UiEl1();
            u.Ui = "abc";

            CallbackResourceHandler.Callback = (s) =>
            {
                return "1845951237";
            };

            Dtf.Core.IEndpoint local = LocalWinEndpoint.Instance;
            Dtf.Core.IEndpoint remote1 = RemoteWinEndpoint.Create("http://localhost/GLIU-D01/");

            Test(local); // UI automation on local machine
            Test(remote1); // UI automation on remote machine
        } 

        static void Test(Dtf.Core.IEndpoint endpoint)
        {
            // launch app
            IAppFactory localAppFactory = endpoint.QueryInterface<IAppFactory>();
            IApp app = localAppFactory.Create(@"Calc.exe");
            app.Launch();
            Thread.Sleep(1000);

            CallbackResourceHandler.Callback = (s) =>
            {
                return "1845951237";
            };

            var localCalc = new CalcUi(endpoint);
            localCalc.Ui.Calculator.One.Invoke();
            localCalc.Ui.Calculator.Plus.Invoke();
            localCalc.Ui.Calculator.Two.Invoke();
            localCalc.Ui.Calculator.Equals.Invoke();
            Debug.Assert(localCalc.Ui.Calculator.Result.GetProperty("Name").Replace("Display is ", string.Empty).Trim().Equals("3"));
            app.Close();
        }
    }
}
