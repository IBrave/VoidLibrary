using MotorTest.Drivers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MotorTest.Devices
{
    public class Device
    {
        public const double ExceptionValue = Double.MaxValue;

        public string name { get; set; }
        public Driver driver { get; set; }
        public Device(string name)
        {
            this.name = name;
        }
        public virtual bool Register()
        {
            return Connection.RegisterDevice(this);
        }
        public virtual bool Open()
        {
            if (driver == null)
                return false;
            else
                return this.driver.Open();
        }
        public virtual bool Close()
        {
            if (driver == null)
                return false;
            else
                return this.driver.Close();
        }
       
        public virtual byte[] ReadWait(int waitMilliSeconds)
        {
            List<byte> resultList = new List<byte>() ;
            if (this.driver == null)
                return null;
            try
            {
                DateTime dtStart = DateTime.Now;
                TimeSpan ts = TimeSpan.Zero;
                while (ts.TotalMilliseconds < waitMilliSeconds)
                {
                    byte[] data = this.driver.Read();
                    if (data != null && data.Length != 0)
                    {
                        resultList.AddRange(data.ToList());
                    }
                    Thread.Sleep(10);
                    ts = DateTime.Now - dtStart;
                }
            }
            catch
            {
                StackTrace st = new StackTrace(new StackFrame(true));
                StackFrame sf = st.GetFrame(0);
                Console.WriteLine(" File: {0},Method: {1},Line Number: {2}", sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());  
 
                return null;
            }
            return resultList.ToArray();
        }

    }
}
