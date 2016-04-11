using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.Core.Devices;
using IsTableBusy.Core.Exceptions;
using IsTableBusy.Core.Tests.LoadData;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model.Audit;
using Xunit;
using NSubstitute;
using IsTableBusy.Core.Places;

namespace IsTableBusy.Core.Tests.Integration.Devices
{
    public class DeviceStateChangerTest : IsTableBusyDatabaseTest, IDisposable
    {
        public DeviceStateChangerTest()
        {
            DateTimeSupplier.Date = new DateTime(2015, 3, 4);
        }

        [Fact]
        public void change_state_to_busy()
        {
            var loader = new DeviceDrivenTestDataLoader(context);
            var loadedData = loader.Load();
            var tableWithFreeDevice = loadedData.TableWithFreeDevice;
            var deviceStateChanger = new DeviceStateChanger(context, Substitute.For<PlacesHubWrapper>());

            deviceStateChanger.SetBusy(tableWithFreeDevice.Device.Guid, true);

            var table = context.Tables.Single(x => x.Id == tableWithFreeDevice.Id);

            table.IsBusy.Should().Be(true);
            table.LastChangeStateDate.Should().Be(DateTimeSupplier.Date);
        }

        [Fact]
        public void audit_changing_state_to_busy()
        {
            var loader = new DeviceDrivenTestDataLoader(context);
            var loadedData = loader.Load();
            var tableWithFreeDevice = loadedData.TableWithFreeDevice;
            var deviceStateChanger = new DeviceStateChanger(context, Substitute.For<PlacesHubWrapper>());

            deviceStateChanger.SetBusy(tableWithFreeDevice.Device.Guid, true);

            var expectedAudit = new TableAudit
            {
                ItemType = AuditItemType.Table,
                ItemId = tableWithFreeDevice.Id,
                Date = DateTimeSupplier.Date,
                NewState = true,
                Event = "State changed"
            };

            var audit = context.Audits.OfType<TableAudit>().Single();
            audit.ShouldBeEquivalentTo(expectedAudit, options => options.Excluding(x => x.Id));

        }

        [Fact]
        public void inform_hub_about_changing_state_to_busy()
        {
            var loader = new DeviceDrivenTestDataLoader(context);
            var loadedData = loader.Load();
            var tableWithFreeDevice = loadedData.TableWithFreeDevice;
            var hubMock = Substitute.For<PlacesHubWrapper>();
            var deviceStateChanger = new DeviceStateChanger(context, hubMock);

            deviceStateChanger.SetBusy(tableWithFreeDevice.Device.Guid, true);

            hubMock.Received().IsBusy(tableWithFreeDevice.Id);
        }

        [Fact]
        public void inform_hub_about_changing_state_to_free()
        {
            var hubMock = Substitute.For<PlacesHubWrapper>();
            var loader = new DeviceDrivenTestDataLoader(context);
            var loadedData = loader.Load();
            var tableWithBusyDevice = loadedData.TableWithBusyDevice;
            var deviceStateChanger = new DeviceStateChanger(context, hubMock);

            deviceStateChanger.SetBusy(tableWithBusyDevice.Device.Guid, false);
            hubMock.Received().IsFree(tableWithBusyDevice.Id);
        }

        [Fact]
        public void change_state_to_free()
        {
            var loader = new DeviceDrivenTestDataLoader(context);
            var loadedData = loader.Load();
            var tableWithBusyDevice = loadedData.TableWithBusyDevice;
            var deviceStateChanger = new DeviceStateChanger(context, Substitute.For<PlacesHubWrapper>());

            deviceStateChanger.SetBusy(tableWithBusyDevice.Device.Guid, false);

            var table = context.Tables.Single(x => x.Id == tableWithBusyDevice.Id);

            table.IsBusy.Should().Be(false);
            table.LastChangeStateDate.Should().Be(DateTimeSupplier.Date);
        }

        [Fact]
        public void audit_changing_state_to_free()
        {
            var loader = new DeviceDrivenTestDataLoader(context);
            var loadedData = loader.Load();
            var tableWithBusyDevice = loadedData.TableWithBusyDevice;
            var deviceStateChanger = new DeviceStateChanger(context, Substitute.For<PlacesHubWrapper>());

            deviceStateChanger.SetBusy(tableWithBusyDevice.Device.Guid, false);

            var expectedAudit = new TableAudit
            {
                ItemType = AuditItemType.Table,
                ItemId = tableWithBusyDevice.Id,
                Date = DateTimeSupplier.Date,
                NewState = false,
                Event = "State changed"
            };

            var audit = context.Audits.OfType<TableAudit>().Single();
            audit.ShouldBeEquivalentTo(expectedAudit, options => options.Excluding(x => x.Id));
        }

        [Fact]
        public void device_is_not_exists()
        {
            var deviceStateChanger = new DeviceStateChanger(context, Substitute.For<PlacesHubWrapper>());

            Action a = () => { deviceStateChanger.SetBusy(Guid.NewGuid(), false); };

            a.ShouldThrow<ChangingDeviceStateException>();
        }

        [Fact]
        public void device_is_not_connected()
        {
            var loader = new DeviceDrivenTestDataLoader(context);
            var loadedData = loader.Load();

            var deviceStateChanger = new DeviceStateChanger(context, Substitute.For<PlacesHubWrapper>());

            Action a = () => { deviceStateChanger.SetBusy(loadedData.NotConnectedDevice.Guid, false); };

            a.ShouldThrow<ChangingDeviceStateException>();
        }
    }
}
