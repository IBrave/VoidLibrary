using System;
using System.Collections.Generic;
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

    class FileLog
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
    }
}
