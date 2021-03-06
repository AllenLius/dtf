﻿using System;
using System.Threading;
using System.Diagnostics;
using System.ServiceProcess;
using System.Configuration.Install;

namespace Dtf.WinDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                if (args.Length != 1)
                {
                    ShowUsage();
                    Environment.Exit(-1);
                }
                string command = args[0].ToLower();
                switch (command)
                {
                    case "install":
                        ManagedInstallerClass.InstallHelper(new string[] { System.Reflection.Assembly.GetExecutingAssembly().Location });
                        break;
                    case "uninstall":
                        ManagedInstallerClass.InstallHelper(new string[] { "/u", System.Reflection.Assembly.GetExecutingAssembly().Location });
                        break;
                    default:
                        ShowUsage();
                        Environment.Exit(-1);
                        break;
                }
            }
            else
            {
                ServiceBase.Run(new WinService());
            }
        }

        static void ShowUsage()
        {
            Console.WriteLine("{0} [install]/[uninstall]", Process.GetCurrentProcess().ProcessName);
        }
    }
}
