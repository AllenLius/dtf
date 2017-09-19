using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dta.Core
{
    public interface IUiNavigable
    {
        IUiNavigable FindFirst(string ui);
    }
}
