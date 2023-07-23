using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotmail_Change_V1._0
{
    class ProcessHelper
    {

        public static void KillAllProcessTree(string processName)
        {
            Process[] chromeDriverProcesses = Process.GetProcessesByName(processName);
            foreach (var chromeDriverProcess in chromeDriverProcesses)
            {
                KillProcessTree(chromeDriverProcess);
            }
        }

        public static void KillProcessTree(System.Diagnostics.Process process)
        {
            string taskkill = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "taskkill.exe");
            using (var procKiller = new System.Diagnostics.Process())
            {
                procKiller.StartInfo.FileName = taskkill;
                procKiller.StartInfo.Arguments = string.Format("/PID {0} /T /F", process.Id);
                procKiller.StartInfo.CreateNoWindow = true;
                procKiller.StartInfo.UseShellExecute = false;
                procKiller.Start();
                procKiller.WaitForExit(1000);
            }
        }

        public static void CloseByName(string processName)
        {
            foreach (Process process in from pr in Process.GetProcesses()
                                        where pr.ProcessName == processName
                                        select pr)
            {
                try
                {
                    process.Kill();
                }
                catch
                {
                }
            }
        }

        public static void CloseChromeDriver()
        {
            foreach (Process process in from pr in Process.GetProcesses()
                                        where pr.ProcessName == "chromedriver"
                                        select pr)
            {
                try
                {
                    process.Kill();
                }
                catch
                {
                }
            }
        }

        public static void CloseChrome()
        {
            foreach (Process process in from pr in Process.GetProcesses()
                                        where pr.ProcessName == "chrome"
                                        select pr)
            {
                try
                {
                    process.Kill();
                }
                catch
                {
                }
            }
        }
    }
}
