using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTableBusy.App.RaspberryPi.Logic
{
    public sealed class RasbperryPi : Device
    {
        public RasbperryPi()
        {
            GreenLight = new Led(5);
            RedLight = new Led(6);
            Button = new ButtonImp(22);
        }
        public Light RedLight { get; private set; }
        public Light GreenLight { get; private set; }
        public Button Button { get; private set; }
    }
}
