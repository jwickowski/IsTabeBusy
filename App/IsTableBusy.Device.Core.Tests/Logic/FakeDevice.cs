using IsTableBusy.Device.Core.Logic;

namespace IsTableBusy.App.RaspberryPi.Tests.Logic
{
    public class FakeDevice : IoTDevice
    {
        public Light RedLight { get; } = new FakeLight();
        public Light GreenLight { get; } = new FakeLight();
        public Button Button { get; } = new FakeButton();
    }
}
