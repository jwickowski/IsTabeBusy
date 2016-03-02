using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;
using Xunit;
namespace IsTableBusy.Core.Tests.Integration
{
    public class DeviceTableConnectorTest : IsTableBusyDatabaseTest, IDisposable
    {
        [Fact]
        public void Connect_table_with_device()
        {
            using (Context ctx = new Context())
            {
                var place = new Place {Name = "place1"};
                var table = new Table {Name = "table1", IsBusy = false, Place = place};
                var device = new Device {Guid = Guid.NewGuid()};
                ctx.Places.Add(place);
                ctx.Devices.Add(device);
                ctx.SaveChanges();

                var connector = new DeviceTableConnector(ctx);
                connector.Connect(table.Id, device.Id);

                var result = ctx.Tables.Single(x => x.Id == table.Id);
                result.DeviceId.Should().Be(device.Id);
            }
        }
    }
}
