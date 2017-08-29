using System.IO.Ports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace MotorTest.Drivers
{
    public class SerialPortDriver : Driver
    {
        public const string PortName = "PortName";
        public const string BaudRate = "BaudRate";
        public const string StopBits = "StopBits";

        SerialPort serialPortInstance;
        const float minWaitSeconds = 0.1f;
        string portName = string.Empty;
        int bandRate = 0;
        int stopBits = 1;

        public SerialPortDriver(string name)
        {
            this.name = name;
            serialPortInstance = new SerialPort();
        }
        public override bool Register()
        {
            return Connection.RegisterDriver(this);
        }
        public override int GetStatus()
        {
            return 0;
        }
        public override bool Open()
        {
            try
            {
                if (this.serialPortInstance.IsOpen == false)
                    this.serialPortInstance.Open();
                this.serialPortInstance.DiscardInBuffer();
                this.serialPortInstance.DiscardOutBuffer();
                return true;
            }
            catch
            {
                StackTrace st = new StackTrace(new StackFrame(true));
                StackFrame sf = st.GetFrame(0);
                Console.WriteLine(" File: {0},Method: {1},Line Number: {2}", sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());  
 
                return false;
            }
        }
        public override bool Close()
        {
            try
            {
                if (this.serialPortInstance.IsOpen == true)
                    this.serialPortInstance.Close();
                return true;
            }
            catch
            {
                StackTrace st = new StackTrace(new StackFrame(true));
                StackFrame sf = st.GetFrame(0);
                Console.WriteLine(" File: {0},Method: {1},Line Number: {2}", sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());  
 
                return false;
            }
        }
        public override bool Send(byte[] data)
        {
            if (data == null || data.Length == 0 || this.serialPortInstance.IsOpen==false)
                return false;
            lock (driverLock)
            {
                try
                {
                    this.ClearOutBuffer();
                    this.serialPortInstance.Write(data, 0, data.Length);
                    return true;
                }
                catch
                {
                    StackTrace st = new StackTrace(new StackFrame(true));
                    StackFrame sf = st.GetFrame(0);
                    Console.WriteLine(" File: {0},Method: {1},Line Number: {2}", sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());  
 
                    Console.WriteLine("Serial Port Send Error!");
                    return false;
                }
            }
        }
        public override byte[] Read()
        {
            if (this.serialPortInstance.IsOpen == false)
                return null;
            lock (driverLock)
            {
                try
                {
                    int n = this.serialPortInstance.BytesToRead;
                    if (n > 0)
                    {
                        byte[] data = new byte[n];//返回命令包
                        this.serialPortInstance.Read(data, 0, data.Length);//读取数据
                        return data;
                    }
                }
                catch
                {
                    return null;
                }
                return null;
            }
        }
        public override void ClearInBuffer()
        {
            if (this.serialPortInstance.IsOpen == false)
                return;
            this.serialPortInstance.DiscardInBuffer();
        }
        public override void ClearOutBuffer()
        {
            if (this.serialPortInstance.IsOpen == false)
                return;
            this.serialPortInstance.DiscardOutBuffer();
        }
        public override bool ParameterMap(string paraName, string paraValue)
        {
            bool result = true;
            switch (paraName)
            {
                case (PortName):
                    {
                        //this.portName = paraValue;
                        this.serialPortInstance.PortName = paraValue;
                        result = true;
                        break;
                    }
                case (BaudRate):
                    {
                        if (Int32.TryParse(paraValue, out this.bandRate) == true)
                        {
                            this.serialPortInstance.BaudRate = this.bandRate;
                            result = true;
                        }
                        break;
                    }
                case (StopBits):
                    {
                        if (Int32.TryParse(paraValue, out this.stopBits) == true)
                        {
                            this.serialPortInstance.StopBits = (StopBits)this.stopBits;
                            result = true;
                        }
                        break;
                    }
                default: break;
            }
            return result;
        }
        public override byte[] FunctionMap(string cmd)
        {
            byte[] result = null;
            switch (cmd)
            {
                case ("1"):
                    {
                        result = test1();
                        break;
                    }
                case ("2"):
                    {
                        result = test2();
                        break;
                    }
                default: break;
            }
            return result;
        }
        public override byte[] FunctionMap(string cmd, byte[] data)
        {
            byte[] result = null;
            switch (cmd)
            {
                case ("1"):
                    {
                        result = test1(data);
                        break;
                    }
                case ("2"):
                    {
                        result = test2(data);
                        break;
                    }
                default: break;
            }
            return result;
        }

        #region FunctionMap功能实现
        private byte[] test1()
        {
            return null;
        }
        private byte[] test2()
        {
            return null;
        }
        private byte[] test1(byte[] data)
        {
            return null;
        }
        private byte[] test2(byte[] data)
        {
            return null;
        }

        #endregion
    }
}
