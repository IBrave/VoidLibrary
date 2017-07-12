using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
#if FRAMEWORK4_0
using System.Linq;
#endif

namespace HarmfulGasMonitoring
{
    public class AppSingleHelper
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        /// <summary>
        /// 查看程序是否已经运行
        /// </summary>
        /// <returns></returns>
        private static Process GetExistProcess()
        {
            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                if ((process.Id != currentProcess.Id) &&
                    (Assembly.GetExecutingAssembly().Location == currentProcess.MainModule.FileName))
                {
                    return process;
                }
            }
            return null;
        }

        /// <summary>
        /// 使程序前端显示
        /// </summary>
        /// <param name="instance"></param>
        private static void SetForegroud(Process instance)
        {
            IntPtr mainFormHandle = instance.MainWindowHandle;
            if (mainFormHandle != IntPtr.Zero)
            {
                ShowWindowAsync(mainFormHandle, 1);
                SetForegroundWindow(mainFormHandle);
            }
        }

        public static bool HadStartedAppAndShowForeground()
        {
            String thisProcessName = Process.GetCurrentProcess().ProcessName;

#if FRAMEWORK4_0
            if (Process.GetProcesses().Count(p => p.ProcessName == thisProcessName) > 1)
            {
#else
            Process[] processes = Process.GetProcesses();
            for (int i = processes.Length - 1; i >= 0; ++i)
            {
                if (processes[i].ProcessName != thisProcessName)
                {
                    continue;
                }
#endif
                Process instance = GetExistProcess();
                if (instance != null)
                {
                    SetForegroud(instance);
                    Application.Exit();
                }
                return true;
            }

            return false;
        }

        public static void KillProcess()
        {
            try
            {
                Process.GetCurrentProcess().Kill();
            }
            catch
            {
            }
        }

    }
}
