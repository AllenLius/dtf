
using System.Threading;
namespace Dtf.Endpoint.Win
{
    public class Keyboard
    {
        public void SendKey(string keys)
        {
            System.Windows.Forms.SendKeys.SendWait(keys);
        }
    }
}
