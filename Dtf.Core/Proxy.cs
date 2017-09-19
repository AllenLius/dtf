using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Dta.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class Proxy
    {
        private static readonly Proxy _WSHttpProxy;
        private static Binding m_binding;

        private static List<WeakReference<object>> _instanceRefs = new List<WeakReference<object>>();
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
            _WSHttpProxy = new Proxy(binding);
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
                object target;
                if (instanceRef.TryGetTarget(out target))
                {
                    if (target is T && _endpoints[i].Equals(server, StringComparison.InvariantCulture))
                    {
                        instance = (T)target;
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
