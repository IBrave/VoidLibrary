using MotorTest.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTest.Devices
{
    public class DeviceRelay : Device
    {
        string cmdStop = "01 06 10 01 00 00";
        string cmdStart1 = "01 05 1F";
        string cmdStart2 = "FF 00";
        public DeviceRelay(string name)
            : base(name)
        { }
        public override bool Open()
        {
            return base.Open();
        }
        public override bool Close()
        {
            return base.Close();
        }
        public override bool Register()
        {
            return base.Register();
        }
        public bool SendAndReceive(byte[] cmdRead)
        {
            byte[] result = null;
            int defaultWaitTimeMilliSeconds = 50;
            try
            {
                this.driver.ClearInBuffer();
                this.driver.ClearOutBuffer();
                this.driver.Send(cmdRead);
                result = this.ReadWait(defaultWaitTimeMilliSeconds);
                if (cmdRead.Length == result.Length)
                {
                    return true;
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
        public bool Start(int id)
        {
            //stop这个方式后面可能需要换掉，因为每次start之前都做了stop
            Stop();
            try
            {
                string index = Convert.ToString(id, 16).PadLeft(2, '0');
                string strCmd = cmdStart1 + index + cmdStart2;
                byte[] cmd = HexStringConverter.StrToHexByteCrc(strCmd);
                if (this.SendAndReceive(cmd) == true)
                    return true;
                else
                    return false;
            }
            catch
            {
                StackTrace st = new StackTrace(new StackFrame(true));
                StackFrame sf = st.GetFrame(0);
                Console.WriteLine(" File: {0},Method: {1},Line Number: {2}", sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());  
 
                return false;
            }
        }
        public void AsyncStart(int id)
        {
            Task.Factory.StartNew(() => Start(id));
        }
        public bool Stop()
        {
            byte[] cmd = HexStringConverter.StrToHexByteCrc(cmdStop);
            try
            {
                if (this.SendAndReceive(cmd) == true)
                    return true;
                else
                    return false;
            }
            catch
            {
                StackTrace st = new StackTrace(new StackFrame(true));
                StackFrame sf = st.GetFrame(0);
                Console.WriteLine(" File: {0},Method: {1},Line Number: {2}", sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());  
 
                return false;
            }
        }
    }
}
