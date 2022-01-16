using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public class RamInfoProvider : IRamInfoProvider
    {
        public double GetFreeRam()
        {
            ManagementObjectSearcher FreeRam = new ManagementObjectSearcher("SELECT FreePhysicalMemory FROM Win32_OperatingSystem");
            double ram = 0;
            foreach (ManagementObject objram in FreeRam.Get())
            {
                ram = (Convert.ToDouble(objram["FreePhysicalMemory"])) / 1024 / 1024;
            }
            return ram;
        }
    }
}
