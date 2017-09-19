using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace Dta.Endpoint.Win
{
    public enum ViewWalkerType
    {
        Content,
        Control,
        Raw
    }

    /// <summary>
    /// Using(new ViewWalker(ViewWalkerType.Raw))
    /// </summary>
    public class WalkerContext : IDisposable
    {
        static Stack<TreeWalker> _treeWalker = new Stack<TreeWalker>();

        static WalkerContext()
        {
            //Default walker
            _treeWalker.Push(TreeWalker.ControlViewWalker);
        }

        public WalkerContext(ViewWalkerType view)
        {
            TreeWalker walker = null;
            switch (view)
            {
                case ViewWalkerType.Content:
                    walker = TreeWalker.ContentViewWalker;
                    break;
                case ViewWalkerType.Control:
                    walker = TreeWalker.ControlViewWalker;
                    break;
                case ViewWalkerType.Raw:
                    walker = TreeWalker.RawViewWalker;
                    break;
                default:
                    throw new NotImplementedException();
            }
            _treeWalker.Push(walker);
        }

        public void Dispose()
        {
            _treeWalker.Pop();
        }

        public static TreeWalker TreeWalker
        {
            get
            {
                return _treeWalker.Peek();
            }
        }
    }
}
