using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    interface IRamInfoProvider
    {
        public double GetFreeRam();
    }
}
