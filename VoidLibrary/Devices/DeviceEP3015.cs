using MotorTest;
using MotorTest.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoidLibrary.Utils;
using System.Threading.Tasks;

namespace VoidLibrary.Devices
{
    public class DeviceEP3015 : Device
    {
        string CMD_READ_ELEC_PARAMS = "01031000000A";
        string CMD_READ_TIME_AND_MULTI_POWER_AND_SPEED = "01031100002AC129";
        string CMD_CLEAR_TIME_AND_STATE = "01 03 1F 55 00 01";
        string CMD_SHAKE_HAND = "01 03 1F 57 00 01";
        string CMD_WAVE = "01 03 1F 57 00 00";

        int defaultWaitTimeMilliSeconds = 200;
        int defaultShortWaitTimeMilliSeconds = 50;

        private const int ReadParamsNum = 5;

        public DeviceEP3015(string name)
            :base(name)
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

        private byte[] ClearInOutBufferAndGetCmdWithCRC16(string strCmd)
        {
            this.driver.ClearInBuffer();
            this.driver.ClearOutBuffer();
            byte[] cmd = HexStringConverter.StrToHexByte(strCmd);
            byte[] cmdWithCRC16 = BytesCheck.GetCRC16Full(cmd, true);

            return cmdWithCRC16;
        }

        /// <summary>
        /// 读 电压、电流、功率、功率因数、频率
        /// </summary>
        /// <returns>电压、电流、功率、功率因数、频率</returns>
        public double[] SendAndReadElecParams()
        {
            byte[] cmdWithCRC16 = ClearInOutBufferAndGetCmdWithCRC16(CMD_READ_ELEC_PARAMS);
            this.driver.Send(cmdWithCRC16);
            byte[] readBytes = this.ReadWait(defaultShortWaitTimeMilliSeconds);

            return ParseElecParams(readBytes);
        }

        /// <summary>
        /// 1100 H  累计时间
        /// 1102 H  第1个功率值
        /// 1104 H  第2个功率值
        /// 1106 H  第3个功率值
        /// 1108 H  第4个功率值
        /// 110A H  第5个功率值
        /// 110C H  第6个功率值
        /// 110E H  第7个功率值
        /// 1110 H  第8个功率值
        /// 1112 H  第9个功率值
        /// 1114 H  第10个功率值
        /// 1116 H  第11个功率值
        /// 1118 H  第12个功率值
        /// 111A H  第13个功率值
        /// 111C H  速度
        /// 111E H  总时间
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public double[] SendAndReadTimeAndMultiPowerAndSpeed()
        {
            byte[] cmdWithCRC16 = ClearInOutBufferAndGetCmdWithCRC16(CMD_READ_TIME_AND_MULTI_POWER_AND_SPEED);
            this.driver.Send(cmdWithCRC16);
            byte[] readBytes = this.ReadWait(defaultWaitTimeMilliSeconds);

            return ParseTimeAndMultiPowerAndSpeed(readBytes);
        }

        public bool SendAndReadClearTimeAndState()
        {
            byte[] cmdWithCRC16 = ClearInOutBufferAndGetCmdWithCRC16(CMD_CLEAR_TIME_AND_STATE);
            this.driver.Send(cmdWithCRC16);
            byte[] readBytes = this.ReadWait(defaultShortWaitTimeMilliSeconds);

            return Bytes.Find(cmdWithCRC16, readBytes) != Bytes.ReturnNotFind;
        }

        public bool SendAndReadShakeHand()
        {
            byte[] cmdWithCRC16 = ClearInOutBufferAndGetCmdWithCRC16(CMD_SHAKE_HAND);
            this.driver.Send(cmdWithCRC16);
            byte[] readBytes = this.ReadWait(defaultShortWaitTimeMilliSeconds);

            return Bytes.Find(cmdWithCRC16, readBytes) != Bytes.ReturnNotFind;
        }

        public bool SendAndReadWave()
        {
            byte[] cmdWithCRC16 = ClearInOutBufferAndGetCmdWithCRC16(CMD_WAVE);
            this.driver.Send(cmdWithCRC16);
            byte[] readBytes = this.ReadWait(defaultShortWaitTimeMilliSeconds);

            return Bytes.Find(cmdWithCRC16, readBytes) != Bytes.ReturnNotFind;
        }

        /// <summary>
        /// 读 电压、电流、功率、功率因数、频率
        /// </summary>
        /// <returns>电压、电流、功率、功率因数、频率</returns>
        private double[] ParseElecParams(byte[] result)
        {
            int frameByteMinNum = 25;

            if (result == null || result.Length < frameByteMinNum || !Crc.CheckCrc16(result))
            {
                return ExceptionValues(ReadParamsNum);
            }

            double volt_value = ParseSingleTypeValue(result, 3, 4);
            double curr_value = ParseSingleTypeValue(result, 7, 4);
            double power_value = ParseSingleTypeValue(result, 11, 4);
            double unknown_value = ParseSingleTypeValue(result, 15, 4);
            double freq_value = ParseSingleTypeValue(result, 19, 4);
            return new double[] { volt_value, curr_value, power_value, unknown_value, freq_value };
        }

        /// <summary>
        /// 1100 H  累计时间
        /// 1102 H  第1个功率值
        /// 1104 H  第2个功率值
        /// 1106 H  第3个功率值
        /// 1108 H  第4个功率值
        /// 110A H  第5个功率值
        /// 110C H  第6个功率值
        /// 110E H  第7个功率值
        /// 1110 H  第8个功率值
        /// 1112 H  第9个功率值
        /// 1114 H  第10个功率值
        /// 1116 H  第11个功率值
        /// 1118 H  第12个功率值
        /// 111A H  第13个功率值
        /// 111C H  速度
        /// 111E H  总时间
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private double[] ParseTimeAndMultiPowerAndSpeed(byte[] result)
        {
            int frameByteMinNum = 88;

            if (result == null || result.Length < frameByteMinNum || !Crc.CheckCrc16(result))
            {
                return ExceptionValues(16);
            }

            double cumulative_time = ParseSingleTypeValue(result, 3, 4);
            double firstpower_value = ParseSingleTypeValue(result, 7, 4);
            double secondpower_value = ParseSingleTypeValue(result, 11, 4);
            double thirdpower_value = ParseSingleTypeValue(result, 15, 4);
            double fourthpower_value = ParseSingleTypeValue(result, 19, 4);
            double fifthpower_value = ParseSingleTypeValue(result, 23, 4);
            double sixthpower_value = ParseSingleTypeValue(result, 27, 4);
            double seventhpower_value = ParseSingleTypeValue(result, 31, 4);
            double eighthpower_value = ParseSingleTypeValue(result, 35, 4);
            double ninthpower_value = ParseSingleTypeValue(result, 39, 4);
            double tenthpower_value = ParseSingleTypeValue(result, 43, 4);
            double eleventhpower_value = ParseSingleTypeValue(result, 47, 4);
            double twelfthpower_value = ParseSingleTypeValue(result, 51, 4);
            double thirteenthpower_value = ParseSingleTypeValue(result, 55, 4);
            double speed_value = ParseSingleTypeValue(result, 59, 4);
            double total_time = ParseSingleTypeValue(result, 63, 4);
            return new double[] { cumulative_time, firstpower_value,secondpower_value, thirdpower_value,fourthpower_value,fifthpower_value,sixthpower_value,seventhpower_value,eighthpower_value,ninthpower_value,tenthpower_value,eleventhpower_value,twelfthpower_value,thirteenthpower_value,speed_value,total_time };
        }

        private double ParseSingleTypeValue(byte[] values, int startIndex, int count)
        {
            byte[] org_params = values.Skip(startIndex).Take(count).ToArray();
            Array.Reverse(org_params);
            return BitConverter.ToSingle(org_params, 0);
        }

        private double[] ExceptionValues(int ReadParamsNum)
        {
            double[] defaultValues = new double[ReadParamsNum];
            for (int i = 0; i < ReadParamsNum; ++i)
            {
                defaultValues[i] = ExceptionValue;
            }
            return defaultValues;
        }

    }

}
