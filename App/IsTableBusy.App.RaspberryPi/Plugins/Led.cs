using Windows.Devices.Gpio;
using IsTableBusy.Device.Core.Logic;

namespace IsTableBusy.App.RaspberryPi.Plugins
{
    public sealed class Led: Light
    {
        private readonly GpioPin pin;
        private GpioPinValue value;

        public Led(int pinNumber)
        {
            var controller = GpioController.GetDefault();
            pin = controller.OpenPin(pinNumber);
            value = GpioPinValue.Low;
            pin.Write(value);
            pin.SetDriveMode(GpioPinDriveMode.Output);
        }

        public void On()
        {
            if (IsOn == false)
            {
                value = GpioPinValue.High;
                pin.Write(value);
            }

        }

        public void Off()
        {
            if (IsOn)
            {
                value = GpioPinValue.Low;
                pin.Write(value);
            }
        }

        public bool IsOn => value == GpioPinValue.High;
    }
}
