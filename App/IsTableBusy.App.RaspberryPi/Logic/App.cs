using IsTableBusy.App.RaspberryPi.Exceptions;

namespace IsTableBusy.App.RaspberryPi.Logic
{
    public sealed class App
    {
        private readonly Device device;
        private readonly ApiClient apiClient;
        private readonly LightManager lightManager;

        private Table Table { get; set; }

        public App(Device device, ApiClient apiClient, LightManager lightManager)
        {
            this.device = device;
            this.apiClient = apiClient;
            this.lightManager = lightManager;

            this.State = AppState.NotStarted;
        }

        private AppState state;

        public AppState State
        {
            get
            {
                return state;
            }
            private set
            {
                state = value;
                lightManager.Apply(state);
            }
        }

        internal void Run()
        {
            try
            {
                RefreshState();
                this.device.Button.Clicked += Button_Clicked;
            }
            catch (ReadingTableException)
            {
                this.State = AppState.Error;
            }
        }

        private void RefreshState()
        {
            Table = apiClient.GetTable();
            if (Table.IsBusy)
            {
                this.State = AppState.Busy;
            }
            else
            {
                this.State = AppState.Free;
            }
        }

        private void Button_Clicked(Button sender, object args)
        {
            try
            {
                HandleClick();
            }
            catch (CachngeTableStateException)
            {
                this.State = AppState.Error;
            }
        }

        private void HandleClick()
        {
            switch (this.State)
            {
                case AppState.NotStarted:
                    {
                        break;
                    }
                case AppState.Working:
                    {
                        break;
                    }
                case AppState.Busy:
                    {
                        UpdateToFreeState();
                        break;
                    }
                case AppState.Free:
                    {
                        UpdateToBusyState();
                        break;
                    }
                case AppState.Error:
                    {
                        RefreshState();
                        break;
                    }
            }
        }

        private void UpdateToFreeState()
        {
            this.State = AppState.Working;
            this.apiClient.SetBusy(false);
            this.State = AppState.Free;

        }

        private void UpdateToBusyState()
        {
            this.State = AppState.Working;
            this.apiClient.SetBusy(true);
            this.State = AppState.Busy;
        }
    }
}

