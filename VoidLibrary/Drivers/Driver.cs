using System;
using System.Collections.Generic;
using System.Text;

namespace MotorTest.Drivers
{
    public abstract class Driver
    {
        public string name { get; set; }
        public object driverLock = new object();
        public abstract bool Register();
        public abstract int GetStatus();
        public abstract bool Open();
        public abstract bool Close();
        public abstract bool Send(byte[] data);
        public abstract byte[] Read();
        public abstract void ClearInBuffer();
        public abstract void ClearOutBuffer();
        public abstract bool ParameterMap(string paraName,string paraValue);
        public abstract byte[] FunctionMap(string cmd);
        public abstract byte[] FunctionMap(string cmd, byte[] data);
    }
}
