using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VoidLibrary.Utils;

namespace VoidLibrary.Helper
{
    public class AppExceptionHelper
    {
        public static AppExceptionHelper Instance = new AppExceptionHelper();

        private AppExceptionHelper()
        {
        }

        public void HandleAppException()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            FileLog.WriteE(FileLog.GetExceptionMsg(e.Exception));
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            FileLog.WriteE(FileLog.GetExceptionMsg((Exception) e.ExceptionObject));
        }

    }
}
