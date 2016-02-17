using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTableBusy.App.RaspberryPi.Logic
{
    public sealed class LightManager
    {
        private readonly Device device;

        public LightManager(Device device)
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
