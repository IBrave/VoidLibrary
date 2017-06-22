using MotorTest.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTest.Drivers
{
    public static class Connection
    {
        public static List<Device> devices = new List<Device>();
        public static List<Driver> drivers = new List<Driver>();
        private static object registerLock = new object();
        public static bool RegisterDevice(Device dev)
        {
            lock (registerLock)
            {
                for (int i = 0; i < drivers.Count; i++)
                {
                    Driver tmpDriver = drivers[i];
                    if (Match(dev, tmpDriver))
                        return true;
                }
                return false;
            }
        }
        public static bool RegisterDriver(Driver drv)
        {
            lock (registerLock)
            {
                drivers.Add(drv);
                return true;
            }
        }

        public static bool Match(Device dev, Driver drv)
        {
            if (dev.name != null && dev.name == drv.name)
            {
                dev.driver = drv;
                return true;
            }
            else
                return false;
        }
    }
}
