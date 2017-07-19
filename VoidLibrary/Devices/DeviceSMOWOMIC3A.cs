using MotorTest;
using MotorTest.Devices;
using System;
using System.Collections.Generic;
using System.Text;

namespace VoidLibrary.Devices
{
    /// <summary>
    /// 力度<求英文>
    /// </summary>
    public class DeviceSMOWOMIC3A : Device
    {
        string CMD_READ = "010300000001840A";
        int defaultWaitTimeMilliSeconds = 200;

        private const int ReadParamsNum = 1;

        public DeviceSMOWOMIC3A(string name)
            : base(name)
        {
            this.name = name;
        }

        public override bool Register()
        {
            return base.Register();
        }

        public override bool Open()
        {
            bool isOpen = base.Open();
            return isOpen;
        }

        public override bool Close()
        {
            return base.Close();
        }

        public double SendAndRead()
        {
            byte[] result = null;
            this.driver.ClearInBuffer();
            this.driver.ClearOutBuffer();
            byte[] cmd = HexStringConverter.StrToHexByte(CMD_READ);
            this.driver.Send(cmd);
            result = this.ReadWait(defaultWaitTimeMilliSeconds);
            return ParseReadData(result);

        }

        private double ParseReadData(byte[] result)
        {
            int frameByteMinNum = 7;
            if (result == null || result.Length < frameByteMinNum || !Crc.CheckCrc16(result))
            {
                return GetExceptionValue(ReadParamsNum);
            }

            double thrust = result[3] * 256 + result[4];
            return thrust;
        }

        private double GetExceptionValue(int readParamsNum)
        {
            return ExceptionValue;
        }

    }

}

