using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            double value = VoidLibrary.Devices.DeviceSHT15.ParseSingleTypeValue(new byte[] { 0X27, 0X10 }, 0, 2);
            Console.WriteLine(value);
            string temdata = System.Text.Encoding.Default.GetString(new byte[] { 0X27, 0X10 });
            Console.WriteLine(temdata);
            Console.ReadKey();

        }
    }
}
