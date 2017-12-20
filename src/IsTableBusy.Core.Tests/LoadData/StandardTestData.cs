using IsTableBusy.EntityFramework.Model;

namespace IsTableBusy.Core.Tests.LoadData
{
    public class StandardTestData
    {
        public Place PlaceWithTwoTables { get; set; }
        public Place PlaceWithoutTable { get; set; }
        public Device NotConnectedDevice { get; set; }
        public Device ConnectedDevice { get; set; }
        public Table TableWithoutDevice { get; set; }
        public Table TableWithDevice { get; set; }
        
    }
}