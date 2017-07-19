using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoidDBLibrary;
using VoidLibrary.Helper;
using VoidLibrary.Utils;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            double value = VoidLibrary.Devices.DeviceSHT15.ParseSingleTypeValue(new byte[] { 0X27, 0X10 }, 0, 2);
            Console.WriteLine(value);
            string temdata = System.Text.Encoding.Default.GetString(new byte[] { 0X27, 0X10 });
            Console.WriteLine(temdata);
             * */
            VoidSqlite3Helper dbHelper = new VoidSqlite3Helper("test");
            dbHelper.Open();
            string sql = "create table highscores (name varchar(20), score int)";
            dbHelper.ExecuteNonQuery(sql);

            WinSysUtil winSysUtil = new WinSysUtil();
            winSysUtil.AdaptSystemPlatform();
            FileUtil.CopyDir(@"C:\Users\liuhao\Desktop\SrcDir\", @"C:\Users\liuhao\Desktop\DestDir");
            AppExceptionHelper.Instance.HandleAppException();
            FileLog.F();
            Console.ReadKey();

        }
    }
}
