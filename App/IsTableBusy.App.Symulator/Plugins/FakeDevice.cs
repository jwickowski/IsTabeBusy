using IsTableBusy.Device.Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;
using IsTableBusy.App.Symulator.Plugins;

namespace IsTableBusy.App.RaspberryPi.Logic
{
    public sealed class FakeDevice : IoTDevice
    {
        public FakeDevice(Windows.UI.Xaml.Controls.Button button, Ellipse redEllypse, Ellipse greenEpEllipse)
        {
            GreenLight = new Led(greenEpEllipse);
            RedLight = new Led(redEllypse);
            Button = new ButtonImp(button);
        }
        public Light RedLight { get; private set; }
        public Light GreenLight { get; private set; }
        public Button Button { get; private set; }
    }
}
