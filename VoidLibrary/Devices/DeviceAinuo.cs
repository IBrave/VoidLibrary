using MotorTest.Devices;
using System;
using System.Collections.Generic;
using System.Text;

namespace VacuumTest.VoidLibrary.Devices
{
    public class DeviceAinuo : Device
    {
        private const int DefaultWaitTimeMilliSeconds = 60;
        // private string CMD_READ_VOLTAGE = "7B 00 08 01 A5 00 AE 7D";
        // private string CMD_WRITE_VOLTAGE = "7B 00 0A 01 5A 00 0B B8 28 7D";
        private byte[] CMD_READ_VOLTAGE = new byte[] { 0X7B, 0X00, 0X08, 0X01, 0XA5, 0X00, 0XAE, 0X7D};
        private byte[] CMD_WRITE_VOLTAGE = new byte[] {0X7B, 0X00, 0X0A, 0X01, 0X5A, 0X00, 0X0B, 0XB8, 0X28, 0X7D};
        private byte[] CMD_STOP_OUTPUT = new byte[] {0X7B, 0X00, 0X08, 0X01, 0X0F, 0X00, 0X18, 0X7D};
        private byte[] CMD_START_OUTPUT = new byte[] { 0X7B, 0X00, 0X08, 0X01, 0X0F, 0XFF, 0X17, 0X7D };

        public DeviceAinuo(string name)
            : base(name)
        {
            this.name = name;
        }

        public double ReadVoltage()
        {
            this.driver.ClearInBuffer();
            this.driver.ClearOutBuffer();
            this.driver.Send(CMD_READ_VOLTAGE);
            byte[] result = this.ReadWait(DefaultWaitTimeMilliSeconds);
            return ParseReadValue(result);
        }

        public float WriteVoltage(float voltage)
        {
            this.driver.ClearInBuffer();
            this.driver.ClearOutBuffer();
            this.driver.Send(BuildCmdWriteVoltage(voltage));
            byte[] result = this.ReadWait(DefaultWaitTimeMilliSeconds);
            return 0;
        }

        public bool StartOutput()
        {
            this.driver.ClearInBuffer();
            this.driver.ClearOutBuffer();
            this.driver.Send(CMD_START_OUTPUT);
            byte[] result = this.ReadWait(DefaultWaitTimeMilliSeconds);
            return Find(CMD_START_OUTPUT, result) >= 0;
        }

        public bool StopOutput()
        {
            this.driver.ClearInBuffer();
            this.driver.ClearOutBuffer();
            this.driver.Send(CMD_STOP_OUTPUT);
            byte[] result = this.ReadWait(DefaultWaitTimeMilliSeconds);
            return Find(CMD_STOP_OUTPUT, result) >= 0;
        }

        private double ParseReadValue(byte[] content)
        {
            int findIndex = Find(content, new byte[] { 0X7B, 0X00, 0X0A, 0X01, 0XA5, 0X00});
            if (findIndex < 0)
            {
                return ExceptionValue;
            }

            if (content.Length - findIndex < 10)
            {
                return ExceptionValue;
            }

            int value = content[findIndex + 6] * 256 + (content[findIndex + 7] & 0XFF);
            return value / 100.0F;
        }

        private byte[] BuildCmdWriteVoltage(float voltage)
        {
            int vol = (int)(voltage * 100);
            CMD_WRITE_VOLTAGE[6] = (byte)((vol / 256) & 0XFF);
            CMD_WRITE_VOLTAGE[7] = (byte)((vol % 256) & 0XFF);
            int sum = 0;
            for (int i = 1; i < 8; ++i)
            {
                sum += CMD_WRITE_VOLTAGE[i];
            }
            CMD_WRITE_VOLTAGE[8] = (byte)(sum & 0XFF);
            return CMD_WRITE_VOLTAGE;
        }

        private int Find(byte[] array, byte[] target)
        {
            int arrayLen = array.Length;
            int targetLen = target.Length;

            if (targetLen == 0) {
                return -1;
            }

            for (int i = 0; i < arrayLen - targetLen + 1;)
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

            return -1;
        }

    }
}
