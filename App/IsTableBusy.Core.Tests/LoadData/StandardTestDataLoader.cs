using System;
using System.Collections.Generic;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;

namespace IsTableBusy.Core.Tests.LoadData
{
    public class StandardTestDataLoader
    {
        private Context context;

        public StandardTestDataLoader(Context context)
        {
            this.context = context;
        }

        public StandardTestData Load()
        {
            var date = new DateTime(2016, 4, 11, 9, 10, 35);

            var result = new StandardTestData();
            var placeWithoutTable = new Place { Name = "PlaceWithoutTable" };
            result.PlaceWithoutTable = placeWithoutTable;
            this.context.Places.Add(placeWithoutTable);

            var placeWithTwoTables = new Place { Name = "PlaceWithTwoTables" };
            result.PlaceWithTwoTables = placeWithTwoTables;
            this.context.Places.Add(placeWithTwoTables);

            var notConnectedDevice = new Device { Guid = Guid.NewGuid() };
            result.NotConnectedDevice = notConnectedDevice;
            this.context.Devices.Add(notConnectedDevice);

            var connectedDevice = new Device { Guid = Guid.NewGuid() };
            result.ConnectedDevice = connectedDevice;
            this.context.Devices.Add(connectedDevice);

            var tableWithoutDevice = new Table
            {
                Name = "TableWithoutDevice",
                IsBusy = false,
                Place = placeWithTwoTables,
                LastChangeStateDate = date
            };
            result.TableWithoutDevice = tableWithoutDevice;
            this.context.Tables.Add(tableWithoutDevice);

            var tableWithDevice = new Table
            {
                Name = "TableWithDevice",
                IsBusy = false,
                Place = placeWithTwoTables,
                Device = connectedDevice,
                LastChangeStateDate = date
            };
            result.TableWithDevice = tableWithDevice;

            placeWithTwoTables.Tables = new List<Table> {tableWithDevice, tableWithoutDevice};
            this.context.Tables.Add(tableWithDevice);

            this.context.SaveChanges();
            return result;
        }
    }
}