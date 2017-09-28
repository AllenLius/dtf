using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Dtf.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class Proxy
    {
        private static readonly Proxy _WSHttpProxy;
        private static Binding m_binding;

        private static List<WeakReference> _instanceRefs = new List<WeakReference>();
        private static List<string> _endpoints = new List<string>();

        //private static string _server;
        
        private const int TimeoutInSeconds = 180;

        static Proxy()
        {
            WSHttpBinding binding = new WSHttpBinding();
            binding.Security.Mode = SecurityMode.None;
            binding.MessageEncoding = WSMessageEncoding.Text;
            TimeSpan timeout = TimeSpan.FromSeconds(TimeoutInSeconds);
            binding.SendTimeout = timeout;
            binding.ReceiveTimeout = timeout;
            binding.CloseTimeout = timeout;
            binding.OpenTimeout = timeout;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.MaxBufferPoolSize = int.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            binding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
            //_WSHttpProxy = new Proxy(binding);
            _WSHttpProxy = new Proxy(new BasicHttpBinding(BasicHttpSecurityMode.None));
        }

        public Proxy(Binding binding)
        {
            m_binding = binding;
        }

        public T GetOrCreate<T>(string server) where T : class
        {
            T instance = null;
            for (int i = 0; i < _instanceRefs.Count; )
            {
                var instanceRef = _instanceRefs[i];
                if (instanceRef.IsAlive)
                {
                    if (instanceRef.Target is T && _endpoints[i].Equals(server, StringComparison.InvariantCulture))
                    {
                        instance = (T)instanceRef.Target;
                    }
                    i++;
                }
                else
                {
                    _instanceRefs.RemoveAt(i);
                    _endpoints.RemoveAt(i);
                }
            }

            if (!server.EndsWith("/"))
            {
                server += "/";
            }
            //append default type name
            server += typeof(T).Name.Substring(1);
            EndpointAddress address = new EndpointAddress(server);
            T t = ChannelFactory<T>.CreateChannel(m_binding, address);
            return t;
        }

        public static Proxy WSHttp
        {
            get
            {
                return _WSHttpProxy;
            }
        }
    }
}
