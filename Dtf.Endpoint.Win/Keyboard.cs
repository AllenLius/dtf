
using System.Threading;
namespace Dta.Endpoint.Win
{
    public class Keyboard
    {
        public void SendKey(string keys)
        {
            System.Windows.Forms.SendKeys.SendWait(keys);
        }
    }
}
