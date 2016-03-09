using IsTableBusy.Device.Core.Logic;

namespace IsTableBusy.App.RaspberryPi.Tests.Logic
{
    public class FakeLight : Light
    {
        public void On()
        {
            IsOn = true;
        }

        public void Off()
        {
            IsOn = false;
        }

        public bool IsOn { get; private set; }
    }
}