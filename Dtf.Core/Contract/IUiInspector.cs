using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Dtf.Core
{
    public interface IUiInspector
    {
        bool Exists();
        bool Exists(TimeSpan timeout);
        byte[] Capture();
        string GetProperty(string name);
        string[] GetProperties();
        string GetUi();
        void Wait(TimeSpan timeout);
    }
}
