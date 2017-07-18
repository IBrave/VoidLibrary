using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.IO;

namespace VoidLibrary.Utils
{
    public partial class WinSysUtil
    {

        // http://www.cnblogs.com/czzju/articles/2482474.html
        /// <summary>
        /// 
        /// </summary>
        /// <returns>true:64  false:32</returns>
        private static bool Detect32or64()
        {
#if FRAMEWORK4_0
            return Environment.Is64BitOperatingSystem;;
#else
            try
            {
                string addressWidth = String.Empty;
                ConnectionOptions mConnOption = new ConnectionOptions();
                ManagementScope mMs = new ManagementScope("\\\\localhost", mConnOption);
                ObjectQuery mQuery = new ObjectQuery("select AddressWidth from Win32_Processor");
                ManagementObjectSearcher mSearcher = new ManagementObjectSearcher(mMs, mQuery);
                ManagementObjectCollection mObjectCollection = mSearcher.Get();
                foreach (ManagementObject mObject in mObjectCollection)
                {
                    addressWidth = mObject["AddressWidth"].ToString();
                }
                return addressWidth == "64";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
#endif
        }

        public void AdaptSystemPlatform()
        {
            if (Detect32or64())
            {
                Use64BitLib();
            }
            else
            {
                Use32BitLib();
            }
        }

        private void Use32BitLib()
        {
            string srcLibDir = GetPlatformLibDir(false);
            string[] libFiles = Directory.GetFiles(srcLibDir);
            if (libFiles == null) {
                return;
            }
            FileUtil.CopyDir(srcLibDir, GetAppLibDir());
        }

        private void Use64BitLib()
        {
            string srcLibDir = GetPlatformLibDir(true);
            string[] libFiles = Directory.GetFiles(srcLibDir);
            if (libFiles == null) {
                return;
            }
            FileUtil.CopyDir(srcLibDir, GetAppLibDir());
        }

        private string GetAppLibDir()
        {
            string appDir = Directory.GetCurrentDirectory();
            return appDir;
        }

        private string GetPlatformLibDir(bool is64BitSystem)
        {
            string appDir = Directory.GetCurrentDirectory();
            string libDir = Path.Combine(appDir, "PlatformLib", is64BitSystem ? "64BitSysLib" : "32BitSysLib");
            if (!Directory.Exists(libDir))
            {
                Directory.CreateDirectory(libDir);
            }
            return libDir;
        }
    }
}
