using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.Core.Exceptions;
using IsTableBusy.Core.Tests.LoadData;
using IsTableBusy.EntityFramework.Model.Audit;
using Xunit;
namespace IsTableBusy.Core.Tests.Integration
{
    public class DeviceTableConnectorTest : IsTableBusyDatabaseTest, IDisposable
    {
        [Fact]
        public void Connect_and_audit_table_with_device()
        {
                DateTimeSupplier.Date = new DateTime(2016, 2, 5);
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();

                var tableId = loadedData.TableWithoutDevice.Id;
                var deviceId = loadedData.NotConnectedDevice.Id;

                var connector = new DeviceTableConnector(context);
                connector.Connect(tableId, deviceId);

                var result = context.Tables.Single(x => x.Id == tableId);
                result.DeviceId.Should().Be(deviceId);

                var audit = context.Audits.OfType<TableAudit>().Single();
                audit.ShouldBeEquivalentTo(new TableAudit
                {
                    Date = DateTimeSupplier.Date,
                    ItemId = tableId,
                    ItemType = AuditItemType.Table,
                    Event = "Table connected with device",
                    DeviceId = deviceId
                }, options => options.Excluding(x => x.Id));
            
        }

        [Fact]
        public void Table_not_exists_then_thow_TableDeviceConnectingException()
        {
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();

                var connector = new DeviceTableConnector(context);
                Action a = () => { connector.Connect(55, loadedData.NotConnectedDevice.Id); };
                a.ShouldThrow<TableDeviceConnectingException>();
        }

        [Fact]
        public void Device_not_exists_then_thow_TableDeviceConnectingException()
        {
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();

                var connector = new DeviceTableConnector(context);
                Action a = () => { connector.Connect(loadedData.TableWithoutDevice.Id, 777); };
                a.ShouldThrow<TableDeviceConnectingException>();
        }

        [Fact]
        public void Table_is_connected_with_different_device_then_thow_TableDeviceConnectingException()
        {
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();

                var connector = new DeviceTableConnector(context);
                Action a = () => { connector.Connect(loadedData.TableWithDevice.Id, loadedData.NotConnectedDevice.Id); };
                a.ShouldThrow<TableDeviceConnectingException>();
        }

        [Fact]
        public void Device_is_connected_with_different_table_then_thow_TableDeviceConnectingException()
        {
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();

                var connector = new DeviceTableConnector(context);
                Action a = () => { connector.Connect(loadedData.TableWithoutDevice.Id, loadedData.ConnectedDevice.Id); };
                a.ShouldThrow<TableDeviceConnectingException>();
        }
    }
}
