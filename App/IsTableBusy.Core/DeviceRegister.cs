using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTableBusy.Core.Exceptions;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;

namespace IsTableBusy.Core
{
    public class DeviceRegister
    {
        private Context context;

        public DeviceRegister(Context context)
        {
            this.context = context;
        }

        public Guid Register(Guid? guid = null)
        {
            if (guid.HasValue == false)
            {
                var newDevice = new Device();
                newDevice.Guid = Guid.NewGuid();
                context.Devices.Add(newDevice);
                context.SaveChanges();
                return newDevice.Guid;
            }

            if (context.Devices.Any(x => x.Guid == guid.Value))
            {
                return guid.Value;
            }

            throw new WrongDeviceRegistrationException();
        }
    }
}
