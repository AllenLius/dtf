﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Dtf.Core
{
    [Pattern("Text")]
    public interface ITextPattern
    {
        void SetText(string text);
    }
}
