using MotorTest;
using MotorTest.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoidLibrary.Utils;

namespace VoidLibrary.Devices
{
    public class DeviceMinilab312 : Device
    {
        private string _cmd_up_stream = "01 06 10 01 00 01";
        private string _cmd_down_stream = "01 06 10 01 00 02";
        private string _cmd_flush_stream = "01 06 10 01 00 04";
        private string _cmd_stop_stream = "01 06 10 01 00 00";

        private int _read_time_out_milliseconds = 50;

        public DeviceMinilab312(string name) 
            : base(name)
        {
            this.name = name;
        }

        public bool OpenUpStreamChannel()
        {
            return InnerSendCmdAndReadRsult(_cmd_up_stream);
        }

        public bool OpenDownStreamChannel()
        {
            return InnerSendCmdAndReadRsult(_cmd_down_stream);
        }

        public bool OpenFlushStreamChannel()
        {
            return InnerSendCmdAndReadRsult(_cmd_flush_stream);
        }

        public bool StopOpenAllChannel()
        {
            return InnerSendCmdAndReadRsult(_cmd_stop_stream);
        }

        internal bool InnerSendCmdAndReadRsult(string unpacked_str_cmd)
        {
            try
            {
                byte[] cmd = CrcUtil.GetCRC16Full(HexStringConverter.StrToHexByte(unpacked_str_cmd), true);
                driver.Send(cmd);
                byte[] result = ReadWait(_read_time_out_milliseconds);

                return Bytes.Find(cmd, result) != Bytes.ReturnNotFind;
            }
            catch (Exception e)
            {
                FileLog.WriteE(unpacked_str_cmd);
                FileLog.WritetExceptionMsg(e);
                return false;
            }
        }

    }
}
