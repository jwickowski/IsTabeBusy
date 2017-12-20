namespace IsTableBusy.Device.Core.Logic
{
    public interface ConfigStorage
    {
        void Save(ConfigData data);
        ConfigData Load();
    }
}