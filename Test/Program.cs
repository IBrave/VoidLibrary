using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.VoidAttribute;
using VoidDBLibrary;
using VoidDBLibrary.Model;
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
            XmlParseHelper xmlParseHelper = new XmlParseHelper();
            xmlParseHelper.GetConfigData();
            VoidSqlite3Helper dbHelper = new VoidSqlite3Helper();
            dbHelper.Create("Test");
            dbHelper.Open();
            EntityDao<TypeTestEntity> entityDao = new EntityDao<TypeTestEntity>();
            entityDao.SetSession(dbHelper);
            entityDao.CreateTable();
            DateTime h = DateTime.Now;
            int num = 3600 * 4 * 24 * 30;
            Object transaction = dbHelper.Begin();
            for (int i = 0; i < 0; ++i)
            {
                TypeTestEntity student = new TypeTestEntity();
                // student.id = i + 1;
                student.name = "set" + i;
                student.int_value = i;
                student.float_value = i / 3.5F;
                student.double_value = i % 2 == 0 ? 0 : i / 3.5;
                student.int16_value = (Int16)( i % 2 == 0 ? 0 : 2);
                student.bool_value = i % 2 == 0;
                student.date_time_value = DateTime.Now;
                student.byte_array_value = new byte[] { 0X11, 0X22 };
                entityDao.Insert(student);
            }
            dbHelper.Commit(transaction);
            DateTime t = DateTime.Now;
            Console.WriteLine("Total:" + (t - h).TotalMilliseconds + " Every Cost:" + ((t - h).TotalMilliseconds / (float)num));
            DateTime dateTime = DateTime.Now;
            entityDao.QueryAll();
            Console.WriteLine((DateTime.Now - dateTime).TotalMilliseconds);

            WinSysUtil winSysUtil = new WinSysUtil();
            winSysUtil.AdaptSystemPlatform();
            FileUtil.CopyDir(@"C:\Users\liuhao\Desktop\SrcDir\", @"C:\Users\liuhao\Desktop\DestDir");
            AppExceptionHelper.Instance.HandleAppException();
            new MainDemo().ShowDialog();
            FileLog.F();
            Console.ReadKey();
        }
    }
}
