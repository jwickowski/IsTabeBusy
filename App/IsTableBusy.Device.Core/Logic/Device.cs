using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTableBusy.App.RaspberryPi.Logic
{
    public interface Device
    {
        Light RedLight { get; }
        Light GreenLight { get; }
        Button Button { get; }
    }
}
