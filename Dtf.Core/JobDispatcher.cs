using System;

namespace Dta.Core
{
    public class JobDispatcher
    {
        private JobProxy m_jobProxy;
        private Uri m_proxyServer;
        private HttpJobListener _jobListener;

        public JobDispatcher(JobProxy jobProxy)
        {
            m_jobProxy = jobProxy;
        }

        public void Start(Uri uri)
        {
            m_proxyServer = uri;
            Log.Default.Trace("Start...");
            _jobListener = new HttpJobListener(uri);
            _jobListener.JobReceived += JobReceived;
            _jobListener.Start();
        }

        void JobReceived(string job)
        {
            string result = ProcessJob(job);
            try
            {
                //Log.Default.Trace("Result:{0}", result);
                _jobListener.SendResult(result);
            }
            catch (Exception ex)
            {
                Log.Default.LogException(ex);
            }
        }

        public void Stop()
        {
            _jobListener.Stop();
            _jobListener.JobReceived -= JobReceived;
        }
        
        private string ProcessJob(string message)
        {
            SoapMessage returnMessage = null;
            try
            {
                Log.Default.Trace("Create SoapCallMessage...");
                SoapCallMessage callMessage = SoapMessage.Deserialize<SoapCallMessage>(message);
                Log.Default.Trace("To:{0}", callMessage.To);
                string toRelative = callMessage.To;
                //toRelative = NetHelper.ToDnsAddress(toRelative);
                toRelative = new Uri(toRelative).AbsolutePath;
                toRelative = toRelative.Substring(m_proxyServer.AbsolutePath.Length);
                toRelative = "/" + toRelative.Replace(m_proxyServer.ToString(), String.Empty); ;
                Log.Default.Trace("toRelative:{0}", toRelative);
                SoapCallProxy callProxy = m_jobProxy.GetProxy(toRelative);
                if (callProxy!=null)
                {
                    Log.Default.Trace("find job handler");
                    return callProxy.Execute(message);
                }
                else
                {
                    Log.Default.Error("Invalid job!");
                    returnMessage = new SoapFaultMessage(SoapCode.Client, "Not implement!");
                }
            }
            catch (Exception ex)
            {
                Log.Default.Error("TODO:every exception here should be fix");
                Log.Default.LogException(ex);
                returnMessage = new SoapFaultMessage(SoapCode.Server, "Framework error, should be fix!\r\n" + ex.GetExceptionText());
            }
            return SoapMessage.Serialize(returnMessage);
        }
    }
}
