using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoidLibrary.Utils
{
    public class LRC
    {
        public static int CalcLRC(byte[] bytes, int index, int count)
        {
            int lrc = 0;
            for (int i = index + count - 1; i >= index; --i)
            {
                lrc -= (bytes[i] & 0XFF);
            }

            return lrc & 0XFF;
        }

        public static int CalcLRC(byte[] bytes)
        {
            return CalcLRC(bytes, 0, bytes.Length) & 0XFF;
        }
    }
}
