
using IsTableBusy.Device.Core.Logic;
using System;
using Windows.Devices.Gpio;
using Windows.Foundation;

namespace IsTableBusy.App.RaspberryPi.Logic
{
    public sealed class ButtonImp : Button
    {
        private readonly GpioPin pin;
        public ButtonImp(int pinNumber)
        {
            var controller = GpioController.GetDefault();
            pin = controller.OpenPin(pinNumber);
            pin.SetDriveMode(GpioPinDriveMode.InputPullUp);
            pin.DebounceTimeout = TimeSpan.FromMilliseconds(50);

            pin.ValueChanged += (s, e) =>
            {
                var value = pin.Read();
                if (value == GpioPinValue.Low)
                {
                    Clicked?.Invoke(this, null);
                }
            };
        }

        public event TypedEventHandler<Button, object> Clicked;
    }
}
