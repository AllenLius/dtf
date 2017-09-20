
namespace Dtf.WinDriver
{
    using Dtf.Core;
    using System;
    using System.Configuration;
    using System.ServiceProcess;
    using Dtf.Endpoint.Win;

    internal class WinService : ServiceBase
    {
        JobDispatcher m_jobDispatcher;

        public WinService()
        {
            CanShutdown = false;
            CanPauseAndContinue = false;
            CanHandleSessionChangeEvent = false;
            CanHandlePowerEvent = false;

            JobProxy jobProxy = new JobProxy();
            jobProxy.AddProxy(new WinAutomation());
            m_jobDispatcher = new JobDispatcher(jobProxy);
        }

        protected override void OnStart(string[] args)
        {
            string hostName = Environment.MachineName;
            string server = ConfigurationManager.AppSettings["Server"];
            Uri uri = new Uri(new Uri(server), hostName);
            m_jobDispatcher.Start(uri);
        }

        protected override void OnStop()
        {
            m_jobDispatcher.Stop();
        }        
    }
}