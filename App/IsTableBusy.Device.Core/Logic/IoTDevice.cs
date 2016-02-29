using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTableBusy.Device.Core.Logic
{
    public interface IoTDevice
    {
        Light RedLight { get; }
        Light GreenLight { get; }
        Button Button { get; }
    }
}
