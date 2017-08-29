#define DEUBG_ON
using MotorTest;
using MotorTest.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoidLibrary.Utils;

namespace VoidLibrary.Devices
{

    public class DeviceABB : Device
    {
        private byte[] _cmd_update_freq = new byte[] { 0X0B, 0X06, 0X00, 0X01, 0X00, 0X00};

        public DeviceABB(string name) 
            : base(name)
        {
            this.name = name;
        }

        public bool UpdateFreq(float freq)
        {
            int freq_map_int_value = (int)(freq * 10) * 40;
            _cmd_update_freq[4] = (byte)((freq_map_int_value & 0XFF00) >> 8);
            _cmd_update_freq[5] = (byte)((freq_map_int_value & 0X00FF));

            try
            {
                byte[] cmd_with_crc = CrcUtil.GetCRC16Full(_cmd_update_freq, true);

#if DEUBG_ON
                FileLog.WriteI("Freq:" + HexStringConverter.ByteToHexString(cmd_with_crc));
#endif

                this.driver.Send(cmd_with_crc);

                byte[] result = ReadWait(50);

                return Bytes.Find(cmd_with_crc, result) != Bytes.ReturnNotFind;
            }
            catch (Exception ex)
            {
                FileLog.WritetExceptionMsg(ex);
            }

            return false;
        }

    }

}
