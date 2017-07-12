using System;
using System.Collections.Generic;
using System.Text;

namespace MotorTest.Devices
{
    public class DeviceSource : Device
    {
        public DeviceSource(string name)
            : base(name)
        { }
        public override bool Register()
        {
            return base.Register();
        }
        public override bool Open()
        {
            return base.Open();
        }
        public override bool Close()
        {
            return base.Close();
        }
        
    }
}
