using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Dtf.Server
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller m_serviceProcessInstaller;
        private ServiceInstaller m_serviceInstaller;

        public ProjectInstaller()
        {
            m_serviceProcessInstaller = new ServiceProcessInstaller();
            m_serviceInstaller = new ServiceInstaller();

            m_serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            m_serviceProcessInstaller.Password = null;
            m_serviceProcessInstaller.Username = null;

            m_serviceInstaller.DisplayName = "DTF Server";
            m_serviceInstaller.ServiceName = "DtfServer";
            m_serviceInstaller.StartType = ServiceStartMode.Automatic;

            Installers.AddRange(new Installer[] { m_serviceProcessInstaller, m_serviceInstaller });
        }
    }
}
