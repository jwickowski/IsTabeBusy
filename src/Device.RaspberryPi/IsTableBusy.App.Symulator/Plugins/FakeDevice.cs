using Windows.UI.Xaml.Shapes;
using IsTableBusy.Device.Core.Logic;

namespace IsTableBusy.App.Symulator.Plugins
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
