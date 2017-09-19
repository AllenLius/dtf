using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dta.Endpoint.Win
{
    using Dta.Core;

    internal class WinTextPattern : ITextPattern
    {
        private Endpoint m_endpoint;

        public WinTextPattern(Endpoint endpoint)
        {
            m_endpoint = endpoint;
        }

        public void SetText(string text)
        {
        }
    }
}
