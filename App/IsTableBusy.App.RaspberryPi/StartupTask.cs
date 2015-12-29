using Windows.ApplicationModel.Background;
using IsTableBusy.App.RaspberryPi.Common;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace IsTableBusy.App.RaspberryPi
{
    public sealed class StartupTask : IBackgroundTask
    {
        private Led greenLed;
        private Led redLed;
        private Button button22;
        private BackgroundTaskDeferral deferral;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();

            greenLed = new Led(5);
            redLed = new Led(6);
            button22 = new Button(22);


            button22.Clicked += (s, e) =>
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
