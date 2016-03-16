using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.Core.Devices;
using IsTableBusy.Core.Exceptions;
using IsTableBusy.Core.Tests.LoadData;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model.Audit;
using Xunit;

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
            using (Context context = new Context())
            {
                var loader = new DeviceDrivenTestDataLoader(context);
                var loadedData = loader.Load();
                var tableWithFreeDevice = loadedData.TableWithFreeDevice;
                var deviceStateChanger = new DeviceStateChanger(context);

                deviceStateChanger.SetBusy(tableWithFreeDevice.Device.Guid, true);

                var table = context.Tables.Single(x => x.Id == tableWithFreeDevice.Id);

                table.IsBusy.Should().Be(true);
            }
        }

        [Fact]
        public void audit_changing_state_to_busy()
        {
            using (Context context = new Context())
            {
                var loader = new DeviceDrivenTestDataLoader(context);
                var loadedData = loader.Load();
                var tableWithFreeDevice = loadedData.TableWithFreeDevice;
                var deviceStateChanger = new DeviceStateChanger(context);

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
        }

        [Fact]
        public void change_state_to_free()
        {
            using (Context context = new Context())
            {
                var loader = new DeviceDrivenTestDataLoader(context);
                var loadedData = loader.Load();
                var tableWithBusyDevice = loadedData.TableWithBusyDevice;
                var deviceStateChanger = new DeviceStateChanger(context);

                deviceStateChanger.SetBusy(tableWithBusyDevice.Device.Guid, false);

                var table = context.Tables.Single(x => x.Id == tableWithBusyDevice.Id);

                table.IsBusy.Should().Be(false);
            }
        }

        [Fact]
        public void audit_changing_state_to_free()
        {
            using (Context context = new Context())
            {
                var loader = new DeviceDrivenTestDataLoader(context);
                var loadedData = loader.Load();
                var tableWithBusyDevice = loadedData.TableWithBusyDevice;
                var deviceStateChanger = new DeviceStateChanger(context);

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
        }

        [Fact]
        public void device_is_not_exists()
        {
            using (Context context = new Context())
            {

                var deviceStateChanger = new DeviceStateChanger(context);

                Action a = () => { deviceStateChanger.SetBusy(Guid.NewGuid(), false); };

                a.ShouldThrow<ChangingDeviceStateException>();
            }
        }

        [Fact]
        public void device_is_not_connected()
        {
            using (Context context = new Context())
            {
                var loader = new DeviceDrivenTestDataLoader(context);
                var loadedData = loader.Load();

                var deviceStateChanger = new DeviceStateChanger(context);

                Action a = () => { deviceStateChanger.SetBusy(loadedData.NotConnectedDevice.Guid, false); };

                a.ShouldThrow<ChangingDeviceStateException>();
            }
        }
    }
}
