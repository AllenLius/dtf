using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dtf.Core
{
    public interface IUiNavigable
    {
        IUiNavigable FindFirst(string ui);
    }
}
