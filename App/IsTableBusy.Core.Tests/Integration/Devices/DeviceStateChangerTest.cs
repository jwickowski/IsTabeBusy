using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IsTableBusy.Core.Devices;
using IsTableBusy.Core.Tests.LoadData;
using IsTableBusy.EntityFramework;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration.Devices
{
    public class DeviceStateChangerTest : IsTableBusyDatabaseTest, IDisposable
    {
        public DeviceStateChangerTest()
        {
         //   DateTimeSupplier.Date = new DateTime(2015, 3, 4);
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
    }
}
