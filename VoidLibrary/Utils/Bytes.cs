using System;
using System.Collections.Generic;
using System.Text;

namespace VoidLibrary.Utils
{
    public class Bytes
    {

        public const int ReturnNotFind = -1;

        public static int Find(byte[] array, byte[] target)
        {
            if (array == null || target == null)
            {
                return ReturnNotFind;
            }

            int arrayLen = array.Length;
            int targetLen = target.Length;

            if (targetLen == 0)
            {
                return ReturnNotFind;
            }

            for (int i = 0; i < arrayLen - targetLen + 1; )
            {
                for (int j = 0; j < targetLen; j++)
                {
                    if (array[i + j] != target[j])  // 可以优化的，调整i的位置 Java String.indexOf
                    {
                        goto outer;
                    }
                }
                return i;
            outer:
                i++;
            }

            return ReturnNotFind;
        }
    }
}
