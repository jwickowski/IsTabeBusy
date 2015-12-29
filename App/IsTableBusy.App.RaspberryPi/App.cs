using IsTableBusy.App.RaspberryPi.Common;

namespace IsTableBusy.App.RaspberryPi
{
    public sealed class App
    {
        private Led redLed;
        private Button button;
        private Led greenLed;

        public void Run()
        {
            greenLed = new Led(5);
            redLed = new Led(6);
            button = new Button(22);

            button.Clicked += (s, e) =>
            {
                ChangeLed();
            };

            ChangeLed();
        }

        private void ChangeLed()
        {
            if (greenLed.IsOn)
            {
                greenLed.Off();
                redLed.Off();
            }
            else
            {
                greenLed.On();
                redLed.On();
            }
        }
    }
}
