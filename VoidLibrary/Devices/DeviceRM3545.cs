using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MotorTest.Devices
{
    public class DeviceRM3545 : Device
    {
        string cmdRead = ":FETCh?\r\n";
        string cmdRead34420A = "MEAS:FRES? 1000, MAX\r\n";
        string cmdRemote3440A = "SYST:REM\r\n";
        string cmdRest3440a = "*RST;*CLS;*OPC?\r\n";
        string cmdQuerySystemErro3440a = "SYST:ERR?\r\n";

        int defaultWaitTimeMilliSeconds = 50;
        public DeviceRM3545(string name)
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
                return Init3440APort();
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
        public double SendAndRead3440A()
        {
            byte[] result = null;
            double defaultValue = -1.0;
            try
            {
                this.driver.ClearInBuffer();
                this.driver.ClearOutBuffer();
                this.driver.Send(Encoding.ASCII.GetBytes(cmdRest3440a));
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
        public bool Init3440APort()
        {
            byte[] result = null;
            try
            {
                this.driver.ClearInBuffer();
                this.driver.ClearOutBuffer();
                this.driver.Send(Encoding.ASCII.GetBytes(cmdRemote3440A));
                result = this.ReadWait(defaultWaitTimeMilliSeconds);
                if(result!=null)
                {
                    this.driver.Send(Encoding.ASCII.GetBytes(cmdRest3440a));
                    result = this.ReadWait(defaultWaitTimeMilliSeconds);
                }
                if (result != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
