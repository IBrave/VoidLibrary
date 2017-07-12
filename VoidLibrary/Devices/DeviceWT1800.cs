using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TmctlAPINet;
#if FRAMEWORK4_0
using System.Linq;
using System.Threading.Tasks;
#endif

namespace MotorTest.Devices
{
    public class DeviceWT1800 : Device
    {
        WTConnection connection = new WTConnection();
        private static TMCTL tmDev = new TMCTL();
        private int elementCount = 0; //相数
        //每次打包发送的命令数目，20个电压电流，转速，转矩,3THd
        public const int itemCount = 25;
        public const int eleCount = 20;
        public const int motorPCount = 3;
        const string wtV = "URMS";
        const string wtI = "IRMS";
        const string wtP = "P";
        const string wtf = "FU";
        const string wtCos = "LAMBDA";
        const string wtSpeed = "SPEED";
        const string wtTorque = "TORQUE";
        const string wtThd = "UTHD";
        string[] FCombos = new string[itemCount] { 
            DeviceWT1800.wtV,DeviceWT1800.wtI,DeviceWT1800.wtP,DeviceWT1800.wtf,DeviceWT1800.wtCos,
            DeviceWT1800.wtV,DeviceWT1800.wtI,DeviceWT1800.wtP,DeviceWT1800.wtf,DeviceWT1800.wtCos,
            DeviceWT1800.wtV,DeviceWT1800.wtI,DeviceWT1800.wtP,DeviceWT1800.wtf,DeviceWT1800.wtCos,
            DeviceWT1800.wtV,DeviceWT1800.wtI,DeviceWT1800.wtP,DeviceWT1800.wtf,DeviceWT1800.wtCos,
            DeviceWT1800.wtSpeed,DeviceWT1800.wtTorque,DeviceWT1800.wtThd,DeviceWT1800.wtThd,DeviceWT1800.wtThd
        };
        const string wtOne = "1";
        const string wtTwo = "2";
        const string wtThree = "3";
        const string wtSum = "SIGMA";
        string[] ECombos = new string[itemCount] { 
            DeviceWT1800.wtOne,DeviceWT1800.wtOne,DeviceWT1800.wtOne,DeviceWT1800.wtOne,DeviceWT1800.wtOne,
            DeviceWT1800.wtTwo,DeviceWT1800.wtTwo,DeviceWT1800.wtTwo,DeviceWT1800.wtTwo,DeviceWT1800.wtTwo,
            DeviceWT1800.wtThree,DeviceWT1800.wtThree,DeviceWT1800.wtThree,DeviceWT1800.wtThree,DeviceWT1800.wtThree,
            DeviceWT1800.wtSum,DeviceWT1800.wtSum,DeviceWT1800.wtSum,DeviceWT1800.wtSum,DeviceWT1800.wtSum,
            string.Empty,string.Empty,DeviceWT1800.wtOne,DeviceWT1800.wtTwo,DeviceWT1800.wtThree
        };
        public DeviceWT1800(string name)
            : base(name)
        {
            this.name = name;
        }
        public int Check_WTSeries(int wire, string adr)
        {
            int m_iID = -1;
            string model;
            int rtn;//return 0 when successed.

            rtn = tmDev.Initialize(wire, adr, ref m_iID);
            if (rtn != 0)
            {
                return rtn;
            }
            //set terminator of the message.
            rtn = tmDev.SetTerm(m_iID, 2, 1);
            if (rtn != 0)
            {
                tmDev.Finish(m_iID);
                return rtn;
            }
            //timeout settings, 1*100ms
            rtn = tmDev.SetTimeout(m_iID, 1);
            if (rtn != 0)
            {
                tmDev.Finish(m_iID);
                return rtn;
            }
            //test the device module connected.
            rtn = tmDev.Send(m_iID, "*IDN?");
            int maxLength = 256;
            int realLength = 0;
            StringBuilder buf;
            buf = new StringBuilder(256);
            rtn = tmDev.Receive(m_iID, buf, maxLength, ref realLength);
            model = buf.ToString();
            //check WTseries
            if (model.Contains("WT18"))
            {
                rtn = 0;
            }
            else
            {
                rtn = 1;
            }
            //timeout settings, 20*100ms
            tmDev.SetTimeout(m_iID, 20);
            tmDev.Finish(m_iID);
            return rtn;
        }
        public string SearchIPAddr()
        {
            DEVICELIST[] listbuff = new DEVICELIST[128];
            DEVICELIST[] list = new DEVICELIST[128];
            int listindex = 0;
            int ret = 0;
            int num = 0;
            int n = 0;

            ret = tmDev.SearchDevices(TMCTL.TM_CTL_VXI11, listbuff, 128, ref num, null);
            for (n = 0; n < num; n++)
            {
                ret = Check_WTSeries(TMCTL.TM_CTL_VXI11, listbuff[n].adr);
                if (ret == 0)
                {
                    list[listindex] = listbuff[n];
                    listindex++;
                }
            }
            if (listindex > 0)
            {
                Console.WriteLine("Device is found");
                //MessageBox.Show("Device is found", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return list[0].adr;
            }
            else
            {
                Console.WriteLine("Device is not found");
                //MessageBox.Show("Device is not found", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return string.Empty;
            }
        }
        public override bool Open()
        {
            string msg = string.Empty;
            int rtn = 0;
            string model; ;
            StringBuilder decode;
            //int len = 256;

            decode = new StringBuilder(256);
            string ipAddress = SearchIPAddr();
            if (string.IsNullOrEmpty(ipAddress))
                return false;
            connection.devAddress = ipAddress;
            connection.wireType = 8;
            if (connection.Initialize() != 0)
            {
                return false;
            }
            model = WTConnection.DevModel;
            model = CutLeft("\n", ref model);
            CutLeft(",", ref model);
            int symbol = model.IndexOf(",") - 3;
            if (model.Substring(0, symbol) != "WT1801" && model.Substring(0, symbol) != "WT1802" && model.Substring(0, symbol) != "WT1803"
            && model.Substring(0, symbol) != "WT1804" && model.Substring(0, symbol) != "WT1805" && model.Substring(0, symbol) != "WT1806")
            {
                return false;
            }
            elementCount = Convert.ToInt32(model.Substring(5, 1));
            if (elementCount <= 0)
                return false;
            //将电压设置为自动模式
            for (int i = 0; i < elementCount; i++)
            {
                msg = ":INPUT:VOLT:AUTO:ELEMENT" + (i + 1).ToString() + " " + "ON";
                rtn = connection.Send(msg);
                if (rtn != 0)
                    return false;
            }
            //将电流设置为自动模式
            for (int i = 0; i < elementCount; i++)
            {
                msg = ":INPUT:CURRENT:AUTO:ELEMENT" + (i + 1).ToString() + " " + "ON";
                rtn = connection.Send(msg);
                if (rtn != 0)
                    return false;
            }
            //设置转速为自动模式
            msg = ":MOTOR:TORQUE:AUTO ON";
            rtn = connection.Send(msg);
            if (rtn != 0)
                return false;
            //设置扭矩为自动模式
            msg = ":MOTOR:SPEED:AUTO ON";
            rtn = connection.Send(msg);
            if (rtn != 0)
                return false;
            //设置读取项目总数
            msg = ":NUMERIC:NORMAL:NUMBER " + itemCount.ToString();
            rtn = connection.Send(msg);
            if (rtn != 0)
                return false;
            //send message detail
            msg = ":NUMERIC:NORMAL:";
            for (int i = 0; i < itemCount; i++)
            {
                msg = msg + "ITEM" + (i + 1).ToString() + " " + FCombos[i];
                msg = msg + "," + ECombos[i];
                if (i != itemCount - 1)
                {
                    msg = msg + ";";
                }
            }
            rtn = connection.Send(msg);
            if (rtn != 0)
            {
                return false;
            }
            return true;
        }
        public void SetCTScaleRatio(double ratio)
        {
            for (int i = 0; i < elementCount; i++)
            {
                string msg = ":INPUT:SCALING:CT:ELEMENT" + (i + 1).ToString() + " " + ratio.ToString();
                int rtn = connection.Send(msg);
                if (rtn != 0)
                    return;
                Thread.Sleep(2000);
            }
            return;
        }
        public void SetTorqueScaleRatio(double ratio)
        {
            string msg = ":MOTor:TORQue:LSCale:AVALue " + ratio.ToString();
            int rtn = connection.Send(msg);
            if (rtn != 0)
                return;
            Thread.Sleep(2000);
        }
        public bool AsyncSetCTScaleRatio(double ratio)
        {
#if FRAMEWORK4_0
            Task.Factory.StartNew(() => SetCTScaleRatio(ratio));
#else
            ThreadPool.QueueUserWorkItem((state0) =>
            {
                SetCTScaleRatio(ratio);
            });
#endif
            return true;
        }
        public bool AsyncSetTorqueScaleRatio(double ratio)
        {

#if FRAMEWORK4_0
            Task.Factory.StartNew(() => SetTorqueScaleRatio(ratio));
#else
            ThreadPool.QueueUserWorkItem((state0) =>
            {
                AsyncSetTorqueScaleRatio(ratio);
            });
#endif
            return true;
        }
        public override bool Close()
        {
            return true;
        }
        public List<string> SendAndRead()
        {
            string msg = ":NUMERIC:NORMAL:VALUE?";
            int rtn = connection.Send(msg);
            if (rtn != 0)
            {
                return null;
            }
            int maxLength = 0;
            int realLength = 0;
            string data = "";
            maxLength = 15 * itemCount;
            rtn = connection.Receive(ref data, maxLength, ref realLength);
            if (rtn != 0)
            {
                return null;
            }
            data = CutLeft("\n", ref data);
#if FRAMEWORK4_0
            List<string> result = data.Split(',').ToList();
#else
            List<string> result = new List<string>(data.Split(','));
#endif
            return result;
        }

        #region Function: CutLeft
        //*********************************************
        /// <summary> Function: CutLeft </summary>
        /// <remarks>
        ///cut the left half to outData,
        ///and the right portion remain in inData.
        /// </remarks>
        /// <example>
        ///symbol:"2", in:"12345" => out:"1", in:"345"
        /// </example>
        //*********************************************
        private string CutLeft(string symbol, ref string inData)
        {
            string outData = inData;
            int pos = inData.IndexOf(symbol);
            if (pos == -1)
            {
                //if no symbol, cut with LF.
                pos = inData.IndexOf((char)10);
            }
            if (pos != -1)
            {
                outData = inData.Substring(0, pos);
                inData = inData.Substring(pos + 1);
            }

            //cut data when harmonics mode
            pos = outData.IndexOf(" ");
            if (pos != -1)
            {
                outData = outData.Substring(pos + 1);
            }
            return outData;
        }
        #endregion
    }

    public class WTConnection
    {
        #region Custem Member Init
        private static int commID = -1;
        private static string model = "";
        private string address;      //address parameter.
        private int commType;     //connection type.
        private int terminator;      //terminator in message.
        private static TMCTL tmDev = new TMCTL();
        public static string DevModel
        {
            get
            {
                return model;
            }
        }
        public string devAddress
        {
            set
            {
                address = value;
            }
        }
        public int wireType
        {
            set
            {
                commType = value;
            }
        }
        public int msgTerminator
        {
            set
            {
                terminator = value;
            }
        }

        #endregion

        #region Constructor
        public WTConnection()
        {
            //tmDev = new TmctlDevice();
            terminator = 2;
        }

        public WTConnection(int port, string addr)
        {
            commType = port;
            address = addr;
            terminator = 1;
        }
        #endregion

        #region Function: Initialize
        //**************************************************
        /// <summary>
        /// Set Connection To The Device
        /// </summary>
        /// <returns></returns>
        //**************************************************
        public int Initialize()
        {
            int rtn;//return 0 when successed.
            rtn = tmDev.Initialize(commType, address, ref commID);
            if (rtn != 0)
            {
                return rtn;
            }

            //set terminator of the message.
            rtn = tmDev.SetTerm(commID, terminator, 1);
            if (rtn != 0)
            {
                tmDev.Finish(commID);
                return rtn;
            }

            //timeout settings, 100*100ms
            rtn = tmDev.SetTimeout(commID, 100);
            if (rtn != 0)
            {
                tmDev.Finish(commID);
                return rtn;
            }

            //test the device module connected.
            rtn = tmDev.Send(commID, "*IDN?");

            int maxLength = 50;
            StringBuilder buf;
            int realLength = 0;
            buf = new StringBuilder(20000);

            rtn = tmDev.Receive(commID, buf, maxLength, ref realLength);
            model = buf.ToString();
            if (rtn != 0)
            {
                //it seems no use to do a finish when rtn != 0.
                tmDev.Finish(commID);
            }
            return rtn;
        }
        #endregion

        #region Function: SetTimeout
        //**************************************************
        /// <summary>
        /// Function: Set Timeout Method
        /// </summary>
        /// <returns></returns>
        //**************************************************
        public int SetTimeout(int timeout)
        {
            return (tmDev.SetTimeout(commID, timeout));
        }
        #endregion

        #region Function: Finish
        //**************************************************
        /// <summary>
        /// Function: Break the Connection Method
        /// </summary>
        /// <returns></returns>
        //**************************************************
        public int Finish()
        {
            return (tmDev.Finish(commID));
        }
        #endregion

        #region Function: Send
        //**************************************************
        /// <summary>
        /// Function: Sending Message Method
        /// </summary>
        /// <returns></returns>
        //**************************************************
        public int Send(string msg)
        {
            return (tmDev.Send(commID, msg));
        }
        public int SendByLength(string msg, int len)
        {
            return (tmDev.SendByLength(commID, msg, len));
        }
        #endregion

        #region Function: Receive
        //**************************************************
        /// <summary>
        /// Function: Receive Message
        /// </summary>
        /// <returns></returns>
        //**************************************************
        public int ReceiveSetup()
        {
            return (tmDev.ReceiveSetup(commID));
        }

        public int Receive(ref string buf, int blen, ref int rlen)
        {
            StringBuilder temp;
            temp = new StringBuilder(20000);
            int rtn = tmDev.Receive(commID, temp, blen, ref rlen);
            buf = temp.ToString();
            return rtn;
        }

        public int ReceiveBlockHeader(ref int rlen)
        {
            return (tmDev.ReceiveBlockHeader(commID, ref rlen));
        }
        public int ReceiveBlockData(ref byte[] buf, int blen, ref int rlen, ref int end)
        {
            int rtn = tmDev.ReceiveBlockData(commID, ref buf[0], blen, ref rlen, ref end);
            return rtn;
        }
        #endregion

        #region Function: GetLastError
        //**************************************************
        /// <summary>
        /// Function: Get Last Error Method
        /// </summary>
        /// <returns></returns>
        //**************************************************
        public int GetLastError()
        {
            return (tmDev.GetLastError(commID));
        }
        #endregion

        #region Function: SetRen
        //**************************************************
        /// <summary>
        /// Function: Set the Remote Status
        /// </summary>
        /// <returns></returns>
        //**************************************************
        //##Function: Set the Remote Status##
        public int SetRen(int flag)
        {
            return (tmDev.SetRen(commID, flag));
        }
        #endregion

        #region Function: GetEncodeSerialNumber
        //**************************************************
        /// <summary>
        /// Function: Get the EncodeSerialNumber
        /// </summary>
        /// <returns></returns>
        //**************************************************
        public int GetEncodeSerialNumber(StringBuilder decode, int len, string src)
        {
            return (tmDev.EncodeSerialNumber(decode, len, src));
        }
        #endregion
    }
}
