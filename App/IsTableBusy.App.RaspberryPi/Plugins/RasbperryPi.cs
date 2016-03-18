using IsTableBusy.Device.Core.Logic;

namespace IsTableBusy.App.RaspberryPi.Plugins
{
    public sealed class RasbperryPi : IoTDevice
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
