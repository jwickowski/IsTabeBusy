namespace IsTableBusy.Device.Core.Logic
{
    public interface ApiClient
    {
        bool GetBusy();

        void SetBusy(bool isBusy);

        void RegisterDevice();
    }
}
