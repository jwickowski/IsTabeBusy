using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.Devices.Gpio;
using Windows.System.Threading;
using Windows.UI.Xaml;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace IsTableBusy.App.RaspberryPi
{
    public sealed class StartupTask : IBackgroundTask
    {
        private GpioPinValue pinValue;
        private GpioPin pin5;
        private GpioPin pin6;
        private BackgroundTaskDeferral deferral = null;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();

            pin5 = InitLed(5);
            pin6 = InitLed(6);
            ThreadPoolTimer.CreatePeriodicTimer(Target, TimeSpan.FromMilliseconds(500));
            

          
        }

        private void Target(ThreadPoolTimer timer)
        {
            ChangeLed();
        }

        private GpioPin InitLed(int pinNumber)
        {
            var controller = GpioController.GetDefault();
            GpioPin pin5 = controller.OpenPin(pinNumber);
            pinValue = GpioPinValue.High;
            pin5.Write(pinValue);
            pin5.SetDriveMode(GpioPinDriveMode.Output);
            return pin5;
        }

        private void ChangeLed()
        {
            if (pinValue == GpioPinValue.High)
            {
                pinValue = GpioPinValue.Low;
                pin5.Write(pinValue);
                pin6.Write(GpioPinValue.High);
            }
            else
            {
                pinValue = GpioPinValue.High;
                pin5.Write(GpioPinValue.High);
                pin6.Write(GpioPinValue.Low);
            }
        }
    }
}
