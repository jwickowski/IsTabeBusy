using IsTableBusy.EntityFramework.Model;

namespace IsTableBusy.Core.Tests.LoadData
{
    public class DeviceDrivenTestData
    {
        public Place Place { get; set; }
        public Table TableWithFreeDevice { get; set; }
        public Table TableWithBusyDevice { get; set; }
        public Device NotConnectedDevice { get; set; }
    }
}
