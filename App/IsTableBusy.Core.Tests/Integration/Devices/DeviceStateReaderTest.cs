using FluentAssertions;
using IsTableBusy.Core.Devices;
using IsTableBusy.Core.Tests.LoadData;
using IsTableBusy.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration.Devices
{
    public class DeviceStateReaderTest
    {
        [Fact]
        public void get_free()
        {
            using (Context context = new Context())
            {
                var loader = new DeviceDrivenTestDataLoader(context);
                var loadedData = loader.Load();
                var tableWithFreeDevice = loadedData.TableWithFreeDevice;
                var deviceStateReader = new DeviceStateReader(context);

                bool result = deviceStateReader.Read(tableWithFreeDevice.Device.Guid);
                result.Should().Be(false);
            }
        }

        [Fact]
        public void get_busy()
        {
            using (Context context = new Context())
            {
                var loader = new DeviceDrivenTestDataLoader(context);
                var loadedData = loader.Load();
                var tableWithBusyDevice = loadedData.TableWithBusyDevice;
                var deviceStateReader = new DeviceStateReader(context);

                bool result = deviceStateReader.Read(tableWithBusyDevice.Device.Guid);
                result.Should().Be(true);
            }
        }
    }
}
