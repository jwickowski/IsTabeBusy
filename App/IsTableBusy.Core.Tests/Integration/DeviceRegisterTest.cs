using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IsTableBusy.Core.Exceptions;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration
{
    public class DeviceRegisterTest : IsTableBusyDatabaseTest, IDisposable
    {

        [Fact]
        public void Register_new_device()
        {
            using (Context context = new Context())
            {
                DeviceRegister deviceRegister = new DeviceRegister(context);
                Guid deviceGuid = deviceRegister.Register();
                deviceGuid.Should().NotBeEmpty();
                context.Devices.Count(x => x.Guid == deviceGuid).Should().Be(1);
            }
        }


        [Fact]
        public void Register_existing_device_and_accept_it()
        {
            using (Context context = new Context())
            {
                Device device = new Device();
                var guid = Guid.NewGuid();
                device.Guid = guid;
                context.Devices.Add(device);
                context.SaveChanges();

                DeviceRegister deviceRegister = new DeviceRegister(context);
                Guid deviceGuid = deviceRegister.Register(guid);
                deviceGuid.Should().NotBeEmpty();
                deviceGuid.Should().Be(guid);
                context.Devices.Count(x => x.Guid == deviceGuid).Should().Be(1);
            }
        }

        [Fact]
        public void Register_existing_device_but_there_is_not_the_guid_then_throw_WrongDeviceRegistrationExceptions()
        {
            using (Context context = new Context())
            {
               
                var guid = Guid.NewGuid();
            

                DeviceRegister deviceRegister = new DeviceRegister(context);

                Action a = () => deviceRegister.Register(guid);
                a.ShouldThrow<WrongDeviceRegistrationException>();

            }
        }
    }
}
