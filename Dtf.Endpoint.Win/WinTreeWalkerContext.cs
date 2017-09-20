using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtf.Endpoint.Win
{
    using Dtf.Core;

    internal class WinTreeWalker : ITreeWalkerContext
    {
        private static Stack<WinTreeWalker> _context = new Stack<WinTreeWalker>();
        private WinAutomation m_winAutomation;
        private Expression m_filter;

        public WinTreeWalker(WinAutomation winAutomation, Expression filter)
        {
            m_winAutomation = winAutomation;
            m_filter = filter;
        }

        public IDisposable Active()
        {
            return new WinTreeWalkerDisposer(this);
        }

        public class WinTreeWalkerDisposer : IDisposable
        {
            private WinTreeWalker m_treeWalker;

            public WinTreeWalkerDisposer(WinTreeWalker treeWalker)
            {
                if (treeWalker == null)
                {
                    throw new ArgumentNullException("treeWalker");
                }
                m_treeWalker = treeWalker;
                _context.Push(m_treeWalker);
                m_treeWalker.m_winAutomation.TreeWalker_Set(m_treeWalker.m_filter);
            }

            ~WinTreeWalkerDisposer()
            {
                Dispose(false);
            }

            public void Dispose()
            {
                Dispose(true);
            }

            private void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _context.Pop();
                    if (_context.Count != 0)
                    {
                        m_treeWalker.m_winAutomation.TreeWalker_Set(_context.Peek().m_filter);
                    }
                }
            }
        }
    }
}