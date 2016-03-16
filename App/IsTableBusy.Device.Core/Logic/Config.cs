using System;

namespace IsTableBusy.Device.Core.Logic
{
    public sealed class Config
    {
        SerializableStorage storage;
        public Config()
        {
            storage = new SerializableStorage("IsTableBusy.config.xml");
        }
        public string ApiUrl => "http://192.168.0.101/api";

        public Guid DeviceGuid
        {
            get
            {
                return storage.Load().DeviceGuid;
            }
            set
            {
                var data = storage.Load();
                data.DeviceGuid = value;
                storage.Save(data);
            }
        }

        public string PlaceName { get; set; }

        public int TableId { get; set; }
    }

    public sealed class ConfigData
    {
        public Guid DeviceGuid { get; set; }
    }
}