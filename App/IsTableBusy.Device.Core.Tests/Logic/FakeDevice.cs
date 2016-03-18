using IsTableBusy.Device.Core.Logic;

namespace IsTableBusy.Device.Core.Tests.Logic
{
    public class FakeDevice : IoTDevice
    {
        public Light RedLight { get { return FakeRedLight; } }
        public Light GreenLight { get { return FakeGreenLight; } }
        public Button Button { get { return FakeButton; } }

        public FakeLight FakeRedLight { get; } = new FakeLight();
        public FakeLight FakeGreenLight { get; } = new FakeLight();
        public FakeButton FakeButton { get; } = new FakeButton();
    }
}
