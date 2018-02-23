using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.Core.Devices;
using IsTableBusy.Core.Exceptions;
using IsTableBusy.Core.Tests.LoadData;
using IsTableBusy.EntityFramework.Model;
using IsTableBusy.EntityFramework.Model.Audit;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration.Devices
{
    public class DeviceRegisterTest : IsTableBusyDatabaseTest, IDisposable
    {
        public DeviceRegisterTest()
        {
            DateTimeSupplier.Date = new DateTime(2015, 3, 4);
        }

        [Fact]
        public void Register_new_device()
        {
                DeviceRegister deviceRegister = new DeviceRegister(context);
                Guid deviceGuid = deviceRegister.Register();
                deviceGuid.Should().NotBeEmpty();
                context.Devices.Count(x => x.Guid == deviceGuid).Should().Be(1);
        }

        [Fact]
        public void Register_existing_device_and_accept_it()
        { 
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();

                DeviceRegister deviceRegister = new DeviceRegister(context);
                Guid deviceGuid = deviceRegister.Register(loadedData.NotConnectedDevice.Guid);
                deviceGuid.Should().Be(loadedData.NotConnectedDevice.Guid);
                context.Devices.Count(x => x.Guid == deviceGuid).Should().Be(1);
        }

        [Fact]
        public void Register_existing_device_but_there_is_not_the_guid_then_throw_WrongDeviceRegistrationExceptions()
        {
                var guid = Guid.NewGuid();

                DeviceRegister deviceRegister = new DeviceRegister(context);

                Action a = () => deviceRegister.Register(guid);
                a.ShouldThrow<WrongDeviceRegistrationException>();
        }

        [Fact]
        public void Audit_registering_new_device()
        {
                DeviceRegister deviceRegister = new DeviceRegister(context);
                Guid deviceGuid = deviceRegister.Register();
                int deviceId = context.Devices.First().Id;

                var expectedAudit = new DeviceAudit
                {
                    ItemType = AuditItemType.Device,
                    ItemId = deviceId,
                    DeviceGuid = deviceGuid,
                    Date = DateTimeSupplier.Date,
                    Event = "Registered new device"
                };

                var audit = context.Audits.OfType<DeviceAudit>().Single();
                audit.ShouldBeEquivalentTo(expectedAudit, options => options.Excluding(x => x.Id));
        }

        [Fact]
        public void Audit_register_existing_device()
        {
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();

                DeviceRegister deviceRegister = new DeviceRegister(context);
                Guid deviceGuid = deviceRegister.Register(loadedData.NotConnectedDevice.Guid);

                var expectedAudit = new DeviceAudit
                {
                    ItemType = AuditItemType.Device,
                    ItemId = loadedData.NotConnectedDevice.Id,
                    DeviceGuid = deviceGuid,
                    Date = DateTimeSupplier.Date,
                    Event = "Registered existing device"
                };

                var audit = context.Audits.OfType<DeviceAudit>().Single();
                audit.ShouldBeEquivalentTo(expectedAudit, options => options.Excluding(x => x.Id));
        }

        [Fact]
        public void Audit_wrong_regisering_device()
        {
                var guid = Guid.NewGuid();

                DeviceRegister deviceRegister = new DeviceRegister(context);

                Action a = () => deviceRegister.Register(guid);
                a.ShouldThrow<WrongDeviceRegistrationException>();

                var expectedAudit = new DeviceAudit
                {
                    ItemType = AuditItemType.Device,
                    ItemId = null,
                    DeviceGuid = guid,
                    Date = DateTimeSupplier.Date,
                    Event = "Not registered device"
                };

                var audit = context.Audits.OfType<DeviceAudit>().Single();
                audit.ShouldBeEquivalentTo(expectedAudit, options => options.Excluding(x => x.Id));
        }
    }
}
