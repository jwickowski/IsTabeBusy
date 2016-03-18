using System;

namespace IsTableBusy.Device.Core.Logic
{
    public sealed class Config
    {
        ConfigStorage _storageImp;
        public Config(ConfigStorage configStorage)
        {
            _storageImp = configStorage;
        }
        public string ApiUrl => "http://istablebusyapi.azurewebsites.net";

        public Guid DeviceGuid
        {
            get
            {
                return _storageImp.Load().DeviceGuid;
            }
            set
            {
                var data = _storageImp.Load();
                data.DeviceGuid = value;
                _storageImp.Save(data);
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