using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtf.Core
{
    public interface ITreeWalkerContextFactory
    {
        ITreeWalkerContext Create();
        ITreeWalkerContext Create(Expression filter);

        ITreeWalkerContext RawContext { get; }
        ITreeWalkerContext ControlContext { get; }
        ITreeWalkerContext ContextContext { get; }
    }
}
