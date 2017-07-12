using MotorTest.Devices;
using System;
using System.Collections.Generic;
using System.Text;

namespace VoidLibrary.Devices
{
    class DeviceModbusRTU:Device
    {
        public string version ;
        public const string cmdWriteOne = "01031100002AC129";
        public const string CmdWriteTwo = "01031F55000193CE";
        public DeviceModbusRTU(string name)
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
            if (isOpen)
            {
                return true;
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
        //public double[] SendAndRead()
        //{
        //    switch (version)
        //    {
        //        case cmdWriteOne:
        //            break;
        //        case CmdWriteTwo:
        //            break;
        //    }
        //    return double
        //}
    }
    
   


}
	 
