using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace MotorTest.Devices
{
    public class DeviceGM10 : Device
    {
        string cmdStart = "ORec,1\r\n";
        string cmdStop = "ORec,0\r\n";
        string cmdReadPrefix = "FData,0,";
        int defaultWaitTimeMilliSeconds = 50;
        public DeviceGM10(string name)
            : base(name)
        {
            this.name = name;
        }
        public override bool Open()
        {
            if (base.Open() == false)
                return false;
            if (this.Start() == false)
                return false;
            return true;
        }
        public override bool Close()
        {
            if (this.Stop() == false)
                return false;
            return base.Close();   
        }
        public bool Start()
        {
            try
            {
                if (this.driver.Send(Encoding.ASCII.GetBytes(cmdStart)) == true)
                {
                    Thread.Sleep(defaultWaitTimeMilliSeconds);
                    if(this.driver.Read() != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            catch
            {
                StackTrace st = new StackTrace(new StackFrame(true));
                StackFrame sf = st.GetFrame(0);
                Console.WriteLine(" File: {0},Method: {1},Line Number: {2}", sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());  
 
                Console.WriteLine("GM10 Open Error!");
                return false;
            }
        }
        public bool Stop()
        {
            try
            {
                if (this.driver.Send(Encoding.ASCII.GetBytes(cmdStop)) == true)
                {
                    Thread.Sleep(defaultWaitTimeMilliSeconds);
                    if(this.driver.Read() != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            catch
            {
                StackTrace st = new StackTrace(new StackFrame(true));
                StackFrame sf = st.GetFrame(0);
                Console.WriteLine(" File: {0},Method: {1},Line Number: {2}", sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());  
 
                Console.WriteLine("GM10 Close Error!");
                return false;
            }
        }
        public override bool Register()
        {
            return base.Register();
        }
        public List<double> SendAndRead(int start, int stop)
        {
            List<double> result = new List<double>();
            string resultString = string.Empty;
            byte[] resultBytes = null;
            int length = stop - start + 1;
            if (start < 1 || stop > 20 || start > stop)
                return result;
            string cmdRead = cmdReadPrefix + start.ToString("0000") + "," + stop.ToString("0000") + "\r\n";
            try
            {
                this.driver.Send(Encoding.ASCII.GetBytes(cmdRead));
                Thread.Sleep(defaultWaitTimeMilliSeconds);
                resultBytes = this.driver.Read();
            }
            catch
            {
                StackTrace st = new StackTrace(new StackFrame(true));
                StackFrame sf = st.GetFrame(0);
                Console.WriteLine(" File: {0},Method: {1},Line Number: {2}", sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());  
 
                Console.WriteLine("GM10 Read Error!");
                return result;
            }
            if (resultBytes == null || resultBytes.Length == 0)
                return result;
            resultString = Encoding.ASCII.GetString(resultBytes);
            string[] temp = Regex.Split(resultString, "\r\n", RegexOptions.IgnoreCase);
            if (temp.Length != stop - start + 1 + 5)
                return null;
            for (int i = 3,count = 0; count < length; i++,count++)
            {
                //将连续的多个空格转换一个一个空格
                temp[i] = Regex.Replace(temp[i].Trim(),"\\s+"," ");
                string[] entry = temp[i].Split(' ');
                if (entry.Length != 4)
                {
                    return null;
                }
                double temprature;
                if (Double.TryParse(entry[3], out temprature) == false)
                    return null;
                result.Add(temprature);
            }
            return result;
        }
    }
}
