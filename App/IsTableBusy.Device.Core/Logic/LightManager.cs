namespace IsTableBusy.Device.Core.Logic
{
    public sealed class LightManager
    {
        private readonly IoTDevice device;

        public LightManager(IoTDevice device)
        {
            this.device = device;
        }

        public void Apply(AppState state)
        {
            switch (state)
            {
                case AppState.Free:
                    {
                        device.GreenLight.On();
                        device.RedLight.Off();
                        break;
                    }
                case AppState.Busy:
                    {
                        device.GreenLight.Off();
                        device.RedLight.On();
                        break;
                    }
                case AppState.NotStarted:
                    {
                        device.GreenLight.Off();
                        device.RedLight.Off();
                        break;
                    }
                case AppState.Error:
                    {
                        device.GreenLight.On();
                        device.RedLight.On();
                        break;
                    }
                case AppState.Working:
                    {
                        device.GreenLight.Off();
                        device.RedLight.Off();
                        break;
                    }
            }
        }
    }
}
