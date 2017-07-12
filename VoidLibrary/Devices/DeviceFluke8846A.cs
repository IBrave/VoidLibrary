using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace MotorTest.Devices
{
    public class DeviceFluke8846A : Device
    {
        string cmdRead = "READ?\r\n";
        string cmdRemote = "SYST:REM\r\n";
        string cmdSetResistorMeasure = "CONFigure:RESistance\r\n";

        int defaultWaitTimeMilliSeconds = 500;
        public DeviceFluke8846A(string name)
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
            bool isOpen= base.Open();
            if(isOpen)
            {
                return Init8846APort();
            }
            else
            {
                return false;
            }
        }
        public override bool Close()
        {
            return base.Close();
        }

        public double SendAndRead()
        {
            byte[] result = null;
            double defaultValue = -1.0;
            try
            {
                this.driver.ClearInBuffer();
                this.driver.ClearOutBuffer();
                this.driver.Send(Encoding.ASCII.GetBytes(cmdRead));
                result = this.ReadWait(defaultWaitTimeMilliSeconds);
            }
            catch
            {
                return defaultValue;
            }
            double ohmValue;
            if (Double.TryParse(Encoding.ASCII.GetString(result), out ohmValue) == false)
                return Double.NaN;
            return ohmValue;
        }
        public bool Init8846APort()
        {
            byte[] result = null;
            try
            {
                this.driver.ClearInBuffer();
                this.driver.ClearOutBuffer();
                this.driver.Send(Encoding.ASCII.GetBytes(cmdRemote));
                result = this.ReadWait(defaultWaitTimeMilliSeconds);
                if(result!=null)
                {
                    this.driver.Send(Encoding.ASCII.GetBytes(cmdSetResistorMeasure));
                    result = this.ReadWait(defaultWaitTimeMilliSeconds);
                    if (result != null)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }
    }
}
