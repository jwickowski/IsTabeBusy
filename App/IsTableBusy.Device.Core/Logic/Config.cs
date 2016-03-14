using System;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;

namespace IsTableBusy.Device.Core.Logic
{
    public sealed  class Config
    {
        public string ApiUrl => "http://192.168.0.101/api";

        public Guid DeviceGuid { get; set; }

        public string PlaceName { get; set; }

        public int TableId { get; set; }
    }
}