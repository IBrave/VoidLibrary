using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VoidLibrary.Utils
{
    public class FileUtil
    {
        public static void CopyDir(string srcFileOrDirtPath, string destDirPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加
                if (destDirPath[destDirPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                {
                    destDirPath += System.IO.Path.DirectorySeparatorChar;
                }
                // 判断目标目录是否存在如果不存在则新建
                if (!System.IO.Directory.Exists(destDirPath))
                {
                    System.IO.Directory.CreateDirectory(destDirPath);
                }

                if (!System.IO.Directory.Exists(srcFileOrDirtPath))
                {
                    if (!File.Exists(srcFileOrDirtPath))
                    {
                        return;
                    }
                    string destFilePath = Path.Combine(destDirPath, System.IO.Path.GetFileName(srcFileOrDirtPath));
                    System.IO.File.Copy(srcFileOrDirtPath, destFilePath, true);

                    return;
                }
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = System.IO.Directory.GetFileSystemEntries(srcFileOrDirtPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (System.IO.Directory.Exists(file))
                    {
                        CopyDir(file, destDirPath + System.IO.Path.GetFileName(file));
                    }
                    // 否则直接Copy文件
                    else
                    {
                        System.IO.File.Copy(file, destDirPath + System.IO.Path.GetFileName(file), true);
                    }
                }
            }
            catch (Exception e)
            {
                FileLog.WritetExceptionMsg(e);
            }
        }
    }
}
