using Windows.Devices.Gpio;

namespace IsTableBusy.App.RaspberryPi.Common
{
    public sealed class Led
    {
        private readonly GpioPin pin;
        private GpioPinValue value;

        public Led(int pinNumber)
        {
            var controller = GpioController.GetDefault();
            pin = controller.OpenPin(pinNumber);
            value = GpioPinValue.High;
            pin.Write(value);
            pin.SetDriveMode(GpioPinDriveMode.Output);
        }

        public void On()
        {
            if (IsOn == false)
            {
                value = GpioPinValue.Low;
                pin.Write(value);
            }

        }

        public void Off()
        {
            if (IsOn)
            {
                value = GpioPinValue.High;
                pin.Write(value);
            }
        }

        public bool IsOn => value == GpioPinValue.Low;
    }
}
