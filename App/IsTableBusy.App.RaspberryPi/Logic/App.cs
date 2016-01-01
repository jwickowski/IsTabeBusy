namespace IsTableBusy.App.RaspberryPi.Logic
{
    public sealed class App
    {
        private readonly Device device;


        public App(Device device)
        {
            this.device = device;
        }


        public void Run()
        {
           

            device.Button.Clicked += (s, e) =>
            {
                ChangeLed();
            };

            ChangeLed();
        }

        private void ChangeLed()
        {
            if ( device.GreenLight.IsOn)
            {
                device.GreenLight.Off();
                device.RedLight.Off();
            }
            else
            {
                device.GreenLight.On();
                device.RedLight.On();
            }
        }
    }
}
