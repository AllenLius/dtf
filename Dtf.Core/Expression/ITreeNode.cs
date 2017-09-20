using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dtf.Core
{
    public interface ITreeNode
    {
        IEnumerable<ITreeNode> Children { get; }
        string Name { get; }
        ITreeNode Parent { get; }
        IEnumerable<string> Properties { get; }
        string this[string propertyName] { get; }
    }
}
