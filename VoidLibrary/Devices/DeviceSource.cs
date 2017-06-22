using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
