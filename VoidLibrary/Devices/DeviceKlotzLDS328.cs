using MotorTest.Devices;
using System;
using System.Collections.Generic;
using System.Text;
using VoidLibrary.Utils;

namespace VoidLibrary.Devices
{
    public class DeviceKlotzLDS328 : Device
    {
        byte[] cmd_shake_hand = Encoding.ASCII.GetBytes("U\r\n");
        byte[] cmd_manual = Encoding.ASCII.GetBytes("b\r\n");
        byte[] cmd_start_work = Encoding.ASCII.GetBytes("d\r\n");
        byte[] cmd_query_state = Encoding.ASCII.GetBytes("M\r\n");
        byte[] cmd_stop_count = Encoding.ASCII.GetBytes("e\r\n");
        byte[] cmd_read_data = Encoding.ASCII.GetBytes("B\r\n");

        int defaultWaitTimeMilliSeconds = 50;

        public DeviceKlotzLDS328(string name)
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

        public bool SendShakeHandAndRead()
        {
            byte[] result = null;
            try
            {
                this.driver.ClearInBuffer();
                this.driver.ClearOutBuffer();
                this.driver.Send(cmd_shake_hand);
                result = this.ReadWait(defaultWaitTimeMilliSeconds);
            }
            catch
            {
                return false;
            }
            return Bytes.Find(result, cmd_shake_hand) != Bytes.ReturnNotFind;
        }

        public bool SendManualAndRead()
        {
            byte[] result = null;
            try
            {
                this.driver.ClearInBuffer();
                this.driver.ClearOutBuffer();
                this.driver.Send(cmd_manual);
                result = this.ReadWait(defaultWaitTimeMilliSeconds);
            }
            catch
            {
                return false;
            }
            return Bytes.Find(result, cmd_manual) != Bytes.ReturnNotFind;
        }

        public bool SendStartCountAndRead()
        {
            byte[] result = null;
            try
            {
                this.driver.ClearInBuffer();
                this.driver.ClearOutBuffer();
                this.driver.Send(cmd_start_work);
                result = this.ReadWait(defaultWaitTimeMilliSeconds);
            }
            catch
            {
                return false;
            }
            return Bytes.Find(result, cmd_start_work) != Bytes.ReturnNotFind;
        }

        public byte[] SendQueryStateAndRead()
        {
            byte[] result = null;
            try
            {
                this.driver.ClearInBuffer();
                this.driver.ClearOutBuffer();
                this.driver.Send(cmd_query_state);
                result = this.ReadWait(defaultWaitTimeMilliSeconds);
            }
            catch
            {
                return new byte[0];
            }
            return result;
        }

        public bool SendStopCountAndRead()
        {
            byte[] result = null;
            try
            {
                this.driver.ClearInBuffer();
                this.driver.ClearOutBuffer();
                this.driver.Send(cmd_stop_count);
                result = this.ReadWait(defaultWaitTimeMilliSeconds);
            }
            catch
            {
                return false;
            }
            return Bytes.Find(result, cmd_stop_count) != Bytes.ReturnNotFind;
        }

        public string SendReadDataAndRead()
        {
            byte[] result = null;
            try
            {
                this.driver.ClearInBuffer();
                this.driver.ClearOutBuffer();
                this.driver.Send(cmd_read_data);
                result = this.ReadWait(defaultWaitTimeMilliSeconds * 4);
            }
            catch
            {
                return "";
            }
            return System.Text.Encoding.Default.GetString(result);
        }

        private double ParseReadValue(byte[] result)
        {
            string asciiValue = Encoding.ASCII.GetString(result);
            int index = asciiValue.IndexOf("\r\n");
            if (index - 3 <= 0)
            {
                return ExceptionValue;
            }
            string asciiWeightValue = asciiValue.Substring(0, index - 3).Trim();
            double outWeightValue;
            if (Double.TryParse(asciiWeightValue, out outWeightValue) == false)
                return ExceptionValue;
            return outWeightValue;
        }
    }
}
