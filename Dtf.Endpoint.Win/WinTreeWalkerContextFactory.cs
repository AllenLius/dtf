using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtf.Endpoint.Win
{
    using Dtf.Core;

    internal class WinTreeWalkerContextFactory : ITreeWalkerContextFactory
    {
        private Core.IEndpoint m_endpoint;

        public WinTreeWalkerContextFactory(Core.IEndpoint endpoint)
        {
            m_endpoint = endpoint;
        }

        public ITreeWalkerContext Create()
        {
            throw new NotImplementedException();
        }

        public ITreeWalkerContext Create(Expression condition)
        {
            throw new NotImplementedException();
        }

        public ITreeWalkerContext RawContext
        {
            get { throw new NotImplementedException(); }
        }

        public ITreeWalkerContext ControlContext
        {
            get { throw new NotImplementedException(); }
        }

        public ITreeWalkerContext ContextContext
        {
            get { throw new NotImplementedException(); }
        }
    }
}