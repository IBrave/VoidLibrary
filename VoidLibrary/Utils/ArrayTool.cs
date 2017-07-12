using System;
using System.Collections.Generic;
using System.Text;

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
