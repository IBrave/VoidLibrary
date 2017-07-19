using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;

namespace MotorTest.Drivers
{
    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int bufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[bufferSize];
        //当前通信接收数据的长度
        public int length = 0;
        //当前通信接收的实际数据
        public byte[] recData = new byte[bufferSize];
        public ManualResetEvent connectDone = new ManualResetEvent(false);
        public ManualResetEvent sendDone = new ManualResetEvent(false);
        public ManualResetEvent receiveDone = new ManualResetEvent(false);

    }
    public class NetDriver : Driver
    {
        IPAddress ip;
        int portNum;
        StateObject state = new StateObject();
        int defaultTimeOutMilliSeconds = 50;
        public NetDriver(string name)
        {
            this.name = name;
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
            state.workSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                state.workSocket.BeginConnect(new IPEndPoint(ip, portNum), new AsyncCallback(ConnectCallBack), state);
                if (state.connectDone.WaitOne(defaultTimeOutMilliSeconds) == false)
                {
                    state.workSocket.Close();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                StackTrace st = new StackTrace(new StackFrame(true));
                StackFrame sf = st.GetFrame(0);
                Console.WriteLine(" File: {0},Method: {1},Line Number: {2}", sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());  
 
                Console.WriteLine("Net Driver Open Error!");
                return false;
            }
        }
        public static void ConnectCallBack(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                state.workSocket.EndConnect(ar);
                state.connectDone.Set();
            }
            catch
            {
                Console.WriteLine("ConnectCallBack Error!");
            }
        }
        public override bool Close()
        {
            try
            {
                state.workSocket.Close();
                state.length = 0;
                return true;
            }
            catch
            {
                StackTrace st = new StackTrace(new StackFrame(true));
                StackFrame sf = st.GetFrame(0);
                Console.WriteLine(" File: {0},Method: {1},Line Number: {2}", sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());  
 
                Console.WriteLine("Net Driver Close Error!");
                return false;
            }
        }
        public override bool Send(byte[] data)
        {
            //每次发送数据前关闭以前的连接
            if (state.workSocket.Connected == false)
            {
                Console.WriteLine("Socket Disconnected!");
                return false;
            }

            if (data == null || data.Length == 0)
            {
                return false;
            }

            try
            {
                //每次先清理缓冲区
                this.ClearOutBuffer();
                state.workSocket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallBack), state);
                if (state.sendDone.WaitOne(defaultTimeOutMilliSeconds) == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                StackTrace st = new StackTrace(new StackFrame(true));
                StackFrame sf = st.GetFrame(0);
                Console.WriteLine(" File: {0},Method: {1},Line Number: {2}", sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());  
 
                Console.WriteLine("NetDriver Send Error!");
                return false;
            }
        }
        public static void SendCallBack(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                int bytesSent = state.workSocket.EndSend(ar);
                state.sendDone.Set();
            }
            catch
            {
                StackTrace st = new StackTrace(new StackFrame(true));
                StackFrame sf = st.GetFrame(0);
                Console.WriteLine(" File: {0},Method: {1},Line Number: {2}", sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());  
 
                Console.WriteLine("SendCallBack Error!");
            }
        }
        public override byte[] Read()
        {
            try
            {   
                if (state.workSocket.Available > 0)
                {
                    state.length = state.workSocket.Receive(state.buffer);
                    if (state.length > 0)
                    {
                        byte[] result = new byte[state.length];
                        Array.Copy(state.buffer, result, state.length);
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                StackTrace st = new StackTrace(new StackFrame(true));
                StackFrame sf = st.GetFrame(0);
                Console.WriteLine(" File: {0},Method: {1},Line Number: {2}", sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());  
 
                Console.WriteLine("Read Error!");
                return null;
            }
        }
        //public override byte[] Read()
        //{
        //    try
        //    {
        //        state.workSocket.BeginReceive(state.buffer, 0, StateObject.bufferSize, 0, new AsyncCallback(ReadCallBack), state);
        //        if (state.receiveDone.WaitOne(defaultTimeOutMilliSeconds) == false)
        //        {
        //            return null;
        //        }
        //        if (state.length > 0)
        //        {
        //            byte[] result = new byte[state.length];
        //            Array.Copy(state.recData, result, state.length);
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("NetDriver Read Error!");
        //        return null;
        //    }
        //}
        //public static void ReadCallBack(IAsyncResult ar)
        //{
        //    try
        //    {
        //        StateObject state = (StateObject)ar.AsyncState;
        //        Socket client = state.workSocket;
        //        int bytesRead = client.EndReceive(ar);
        //        if (bytesRead > 0)
        //        {
        //            if (client.Available > 0)
        //                client.BeginReceive(state.buffer, 0, StateObject.bufferSize, 0, new AsyncCallback(ReadCallBack), state);
        //            else
        //            {
        //                state.receiveDone.Set();
        //            }
        //            if (state.length + bytesRead > StateObject.bufferSize)
        //                return;
        //            for (int i = state.length, count = 0; count < bytesRead; i++, count++)
        //            {
        //                state.recData[i] = state.buffer[count];
        //            }
        //            state.length += bytesRead;
        //        }              
        //    }
        //    catch
        //    {
        //        Console.WriteLine("ReadCallBack Error!");
        //    }
        //}

        public override void ClearInBuffer()
        {
            return;
        }
        public override void ClearOutBuffer()
        {
            for (int i = 0; i < 3; i++)
            {
                this.Read();
            }
            return;
        }
        public override bool ParameterMap(string paraName, string paraValue)
        {
            bool result = false;
            switch (paraName)
            {
                case ("IP"):
                    {
                        if (IPAddress.TryParse(paraValue, out this.ip) == true)
                            result = true;
                        break;
                    }
                case ("Port"):
                    {
                        if (Int32.TryParse(paraValue, out this.portNum) == true)
                            result = true;
                        break;
                    }
                default: break;
            }
            return result;
        }
        public override byte[] FunctionMap(string cmd)
        {
            throw new NotImplementedException();
        }
        public override byte[] FunctionMap(string cmd, byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
