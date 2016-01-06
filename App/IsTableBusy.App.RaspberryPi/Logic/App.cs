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
                Table =  apiClient.GetTable();
                if (Table.IsBusy)
                {
                    this.State = AppState.Busy;
                }
                else
                {
                    this.State = AppState.Free;
                }
            }
            catch (ReadingTableException)
            {
                this.State = AppState.Error;
            }
        }
    }
}

