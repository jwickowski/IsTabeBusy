using System;
using Windows.Devices.Gpio;
using Windows.Foundation;

namespace IsTableBusy.App.RaspberryPi.Common
{
    public sealed class Button
    {
        private readonly GpioPin pin;
        public Button(int pinNumber)
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
        
        public event TypedEventHandler<Button,object> Clicked;
    }
}
