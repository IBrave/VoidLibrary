using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoidLibrary.Utils
{
    public class ArrayTool
    {
        public static byte[] EmptyByte = new byte[0];

        public static string ByteToHexString(byte[] data)
        {
            string hex = BitConverter.ToString(data).Replace("-", " ");
            return ByteToHexString(data, " ");
        }

        public static string ByteToHexString(byte[] data, string separated)
        {
            string hex = BitConverter.ToString(data).Replace("-", separated);
            return hex;
        }
    }
}
