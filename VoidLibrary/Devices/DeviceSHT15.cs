using MotorTest;
using MotorTest.Devices;
using System;
using System.Collections.Generic;
using System.Text;
using VoidLibrary.Utils;

namespace VoidLibrary.Devices
{
    /// <summary>
    /// 温湿度大气压
    /// </summary>
    public class DeviceSHT15 : Device
    {
        string CMD_READ = "010400000003";
        int defaultWaitTimeMilliSeconds = 500;

        private const int ReadParamsNum = 3;

        public DeviceSHT15(string name)
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
            if (isOpen)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Close()
        {
            bool isClose = base.Close();
            if (isClose)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public double [] SendAndRead()
        {
            byte[] result = null;
            double[] defaultValue = ExceptionValues();

            try
            {
                this.driver.ClearInBuffer();
                this.driver.ClearOutBuffer();
                byte[] cmd = HexStringConverter.StrToHexByte(CMD_READ);
                byte[] cmdWithCRC16 = CrcUtil.GetCRC16Full(cmd, true);
                this.driver.Send(cmdWithCRC16);
                result = this.ReadWait(defaultWaitTimeMilliSeconds);
            }
            catch
            {
                return defaultValue;
            }
            return ParseReadValue(result);
        }

        private double[] ExceptionValues()
        {
            double[] defaultValue = new double[ReadParamsNum];
            for (int i = 0; i < ReadParamsNum; ++i)
            {
                defaultValue[i] = ExceptionValue;
            }
            return defaultValue;
        }

        public double [] ParseReadValue(byte[] result)
        {
            //TODO 假设result是完整的数据，否则得在result中找完整的数据帧片段
            //张工 的表里是否会出现-1000；？？？
            int frameByteMinNum = 11;
            if (result == null || result.Length < frameByteMinNum || !Crc.CheckCrc16(result))
            {
                return ExceptionValues();
            }

            int frameHeadIndex = 0;
            double tempValue = ParseSingleTypeValue(result, frameHeadIndex + 3, 2);
            double humValue = ParseSingleTypeValue(result, frameHeadIndex + 5, 2);
            double atmValue = ((result[frameHeadIndex + 7] & 0XFF) * 256 + result[frameHeadIndex + 8] & 0XFF) / 10.0;

            return new double[] { tempValue, humValue, atmValue };
        }

        public double ParseSingleTypeValue(byte[] values, int startIndex, int count)
        {
            int value = 0;
            int baseNum = 1;
            for (int i = startIndex + count - 1; i >= startIndex; --i)
            {
                value += (values[i] & 0XFF) * baseNum;
                baseNum *= 256;
            }
            return value / 10.0;
        }
            
    }
}
