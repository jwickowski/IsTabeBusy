using System;
using System.Linq;
using IsTableBusy.Core.Exceptions;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;

namespace IsTableBusy.Core.Devices
{
    public class DeviceRegister
    {
        private Context context;
        private DeviceRegistrationAuditer auditer;
        public DeviceRegister(Context context)
        {
            this.context = context;
            this.auditer = new DeviceRegistrationAuditer(context);
        }

        public Guid Register(Guid? guid = null)
        {
            if (guid.HasValue == false)
            {
                var newDevice = new Device();
                newDevice.Guid = Guid.NewGuid();
                context.Devices.Add(newDevice);
                context.SaveChanges();
                auditer.AuditRegisteringNewDevice(newDevice);
                return newDevice.Guid;
            }
            var device = context.Devices.FirstOrDefault(x => x.Guid == guid.Value);
            if (device != null)
            {
                auditer.AuditRegisteringExistingDevice(device);
                return guid.Value;
            }

            auditer.AuditWrongRegistered(guid.Value);
            throw new WrongDeviceRegistrationException();
        }
    }
}
