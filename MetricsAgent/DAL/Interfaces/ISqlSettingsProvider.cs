﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Interfaces
{
    public interface ISqlSettingsProvider
    {
        string GetConnectionString();
    }
}
