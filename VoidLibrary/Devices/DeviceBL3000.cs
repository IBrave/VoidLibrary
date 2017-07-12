using MotorTest.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoidLibrary.Devices
{

    public class DeviceBL3000 : Device
    {
        string cmdReadStableValue = "p";
        string cmdReadRealTime = "#";
        string cmdPeeled = "t";

        int defaultWaitTimeMilliSeconds = 500;
        public DeviceBL3000(string name)
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
            double defaultValue = Double.MinValue;
            try
            {
                this.driver.ClearInBuffer();
                this.driver.ClearOutBuffer();
                this.driver.Send(Encoding.ASCII.GetBytes(cmdReadStableValue));
                result = this.ReadWait(defaultWaitTimeMilliSeconds);
            }
            catch
            {
                return defaultValue;
            }
            return ParseReadValue(result);
        }

        public double Peeled()
        {
            byte[] result = null;
            double defaultValue = Double.MinValue;
            try
            {
                this.driver.ClearInBuffer();
                this.driver.ClearOutBuffer();
                this.driver.Send(Encoding.ASCII.GetBytes(cmdPeeled));
                result = this.ReadWait(defaultWaitTimeMilliSeconds);
            }
            catch
            {
                return defaultValue;
            }

            return ParseReadValue(result);
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
