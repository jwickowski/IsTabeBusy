using FluentAssertions;
using IsTableBusy.Core.Devices;
using IsTableBusy.Core.Exceptions;
using IsTableBusy.Core.Tests.LoadData;
using System;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration.Devices
{
    public class DeviceStateReaderTest : IsTableBusyDatabaseTest, IDisposable
    {
        [Fact]
        public void get_free()
        {
            var loader = new DeviceDrivenTestDataLoader(context);
            var loadedData = loader.Load();
            var tableWithFreeDevice = loadedData.TableWithFreeDevice;
            var deviceStateReader = new DeviceStateReader(context);

            bool result = deviceStateReader.Read(tableWithFreeDevice.Device.Guid);
            result.Should().Be(false);
        }

        [Fact]
        public void get_busy()
        {
            var loader = new DeviceDrivenTestDataLoader(context);
            var loadedData = loader.Load();
            var tableWithBusyDevice = loadedData.TableWithBusyDevice;
            var deviceStateReader = new DeviceStateReader(context);

            bool result = deviceStateReader.Read(tableWithBusyDevice.Device.Guid);
            result.Should().Be(true);
        }

        [Fact]
        public void wrong_guid()
        {
            var deviceStateReader = new DeviceStateReader(context);

            Action a = () =>
            {
                deviceStateReader.Read(Guid.NewGuid());
            };
            a.ShouldThrow<ReadingDeviceStateException>();
        }

        [Fact]
        public void not_connected_device()
        {
            var loader = new DeviceDrivenTestDataLoader(context);
            var loadedData = loader.Load();
            var deviceStateReader = new DeviceStateReader(context);

            Action a = () =>
            {
                deviceStateReader.Read(loadedData.NotConnectedDevice.Guid);
            };
            a.ShouldThrow<ReadingDeviceStateException>();
        }
    }
}
