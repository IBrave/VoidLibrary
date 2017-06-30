using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoidLibrary.Utils
{
    public enum LogLevel {
        ERROR = 1,
        WARNING = 2,
        INFO = 3
    }

    public class FileLog
    {
        private static FileLog instance = new FileLog();

        private LogLevel mWriteToFileMaxLogLevel;

        private string mFileDir;
        private string mFileName;
        private string mFilePath;

        private FileLog()
        {
            mWriteToFileMaxLogLevel = LogLevel.INFO;
            mFileName = DateTime.Now.ToString("D") + ".log";
            mFileDir = ".\\log\\";
            mFilePath = mFileDir + mFileName;
        }

        private static string GeLogLevelSymbol(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.ERROR:
                    return "E";
                case LogLevel.WARNING:
                    return "W";
                case LogLevel.INFO:
                    return "I";
                default:
                    return "I";
            }
        }

        public static void Write(LogLevel level, string msg)
        {
            if (level <= instance.mWriteToFileMaxLogLevel)
            {
                string headTime = DateTime.Now.ToString("M") + DateTime.Now.ToString("T") + " (" + GeLogLevelSymbol(level) + "): ";
                string tailCRLF = "\r\n";
                Write(headTime + msg + tailCRLF);
                Console.WriteLine(headTime + msg);
            }
        }

        public static void WriteI(string msg)
        {
            Write(LogLevel.INFO, msg);
        }

        public static void WriteW(string msg)
        {
            Write(LogLevel.WARNING, msg);
        }

        public static void WriteE(string msg)
        {
            Write(LogLevel.ERROR, msg);
        }

        public static void Write(string content)
        {
            try
            {
                if (!Directory.Exists(instance.mFileDir))
                {
                    Directory.CreateDirectory(instance.mFileDir);
                }

                File.AppendAllText(instance.mFilePath, content, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void F()
        {
            // StackTrace st = new StackTrace(new StackFrame(true));
            StackTrace st = new StackTrace(true);
            StackFrame sf = st.GetFrame(1);
            Console.WriteLine("File: {0}, Method: {1}, Line Number: {2}", sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());
        }

        public static string GetExceptionMsg(Exception ex)
        {
            // StackTrace trace = new StackTrace(ex, true);
            // StackFrame frame = trace.GetFrame(0);
            StringBuilder msgBuilder = new StringBuilder();
            msgBuilder.Append("异常信息：").Append(ex.Message).Append("\n\t");
            msgBuilder.Append("异常类型：").Append(ex.GetType().Name).Append("\n\t");
            msgBuilder.Append("异常对象：").Append(ex.Source).Append("\n\t");
            msgBuilder.Append("调用堆栈：").Append(ex.StackTrace.Trim()).Append("\n\t");
            msgBuilder.Append("触发方法：").Append(ex.TargetSite).Append("");
            return msgBuilder.ToString();
        }

        public static void WritetExceptionMsg(Exception ex)
        {
            FileLog.WriteE(GetExceptionMsg(ex));
        }

    }
}
