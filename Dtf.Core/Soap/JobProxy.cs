using System.Collections.Generic;

namespace Dtf.Core
{
    public class JobProxy
    {
        private Dictionary<string, SoapCallProxy> m_soapCallProxies;

        public JobProxy()
        {
            m_soapCallProxies = new Dictionary<string, SoapCallProxy>();
        }

        public void AddProxy(object instance)
        {
            SoapCallProxy proxy = new SoapCallProxy(instance);
            string relativeAddress = "/" + instance.GetType().Name.ToUpper();
            m_soapCallProxies.Add(relativeAddress, proxy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativeAddress">relative To address start with '/', refer to soap addressing: http://www.w3.org/2005/08/addressing</param>
        /// <returns></returns>
        public SoapCallProxy GetProxy(string relativeAddress)
        {
            SoapCallProxy callProxy = null;
            m_soapCallProxies.TryGetValue(relativeAddress.ToUpper(), out callProxy);
            return callProxy;
        }
    }
}
