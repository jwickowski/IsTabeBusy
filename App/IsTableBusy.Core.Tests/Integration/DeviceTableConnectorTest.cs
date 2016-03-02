using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.Core.Exceptions;
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
                ctx.Tables.Add(table);
                ctx.Devices.Add(device);
                ctx.SaveChanges();

                var connector = new DeviceTableConnector(ctx);
                connector.Connect(table.Id, device.Id);

                var result = ctx.Tables.Single(x => x.Id == table.Id);
                result.DeviceId.Should().Be(device.Id);
            }
        }

        [Fact]
        public void Table_not_exists_then_thow_TableDeviceConnectingException()
        {
            using (Context ctx = new Context())
            {
                var place = new Place {Name = "place1"};
                var table = new Table {Name = "table1", IsBusy = false, Place = place};
                var device = new Device {Guid = Guid.NewGuid()};
                ctx.Tables.Add(table);
                ctx.Devices.Add(device);
                ctx.SaveChanges();

                var connector = new DeviceTableConnector(ctx);
                Action a = () => { connector.Connect(55, device.Id); };
                a.ShouldThrow<TableDeviceConnectingException>();
            }
        }

        [Fact]
        public void Device_not_exists_then_thow_TableDeviceConnectingException()
        {
            using (Context ctx = new Context())
            {
                var place = new Place { Name = "place1" };
                var table = new Table { Name = "table1", IsBusy = false, Place = place };
                var device = new Device { Guid = Guid.NewGuid() };
                ctx.Tables.Add(table);
                ctx.Devices.Add(device);
                ctx.SaveChanges();

                var connector = new DeviceTableConnector(ctx);
                Action a = () => { connector.Connect(table.Id, 777); };
                a.ShouldThrow<TableDeviceConnectingException>();
            }
        }

        [Fact]
        public void Table_is_connected_with_different_device_then_thow_TableDeviceConnectingException()
        {
            using (Context ctx = new Context())
            {
                var connectedDevice = new Device { Guid = Guid.NewGuid() };
                var freeDevice = new Device { Guid = Guid.NewGuid() };
                var place = new Place { Name = "place1" };
                var table = new Table { Name = "table1", IsBusy = false, Place = place, Device = connectedDevice };
                
                ctx.Tables.Add(table);
                ctx.Devices.Add(freeDevice);
                ctx.SaveChanges();

                var connector = new DeviceTableConnector(ctx);
                Action a = () => { connector.Connect(table.Id, freeDevice.Id); };
                a.ShouldThrow<TableDeviceConnectingException>();
            }
        }
    }
}
