using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTest
{
    public static class HexStringConverter
    {
        public static byte[] StrToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString = "0" + hexString;
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        public static byte[] StrToHexByteCrc(string hexString)
        {
            byte[] result = StrToHexByte(hexString);
            int crcResult = Crc.Crc16(result, result.Length);
            List<byte> resultList = result.ToList();
            resultList.Add((byte)(crcResult));
            resultList.Add((byte)(crcResult >> 8));
            result = resultList.ToArray();
            return result;
        }
        public static string ByteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
        public static string ByteToHexStr(List<byte> data)
        {
            if (data == null)
                return string.Empty;
            return ByteToHexStr(data.ToArray());
        }
    }
}

