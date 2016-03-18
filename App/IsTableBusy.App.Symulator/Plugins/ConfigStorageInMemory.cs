using IsTableBusy.Device.Core.Logic;

namespace IsTableBusy.App.Symulator.Plugins
{
    public sealed class ConfigStorageInMemory : ConfigStorage
    {
        private ConfigData data = new ConfigData();
        public void Save(ConfigData data)
        {
            this.data = data;
        }
        public ConfigData Load()
        {
            return data;
        }
    }
}
