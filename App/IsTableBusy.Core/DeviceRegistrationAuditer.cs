using System;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;
using IsTableBusy.EntityFramework.Model.Audit;

namespace IsTableBusy.Core
{
    internal class DeviceRegistrationAuditer
    {
        private Context context;

        internal DeviceRegistrationAuditer(Context context)
        {
            this.context = context;
        }

        internal void AuditWrongRegistered(Guid guid)
        {
            var audit = PrepareAudit();
            audit.DeviceGuid = guid;
            audit.Event = "Not registered device";
            context.Audits.Add(audit);
            context.SaveChanges();
        }

        private DeviceAudit PrepareAudit(Device device = null)
        {
            var audit = new DeviceAudit
            {
                Date = DateTimeSupplier.Date,
                ItemType = AuditItemType.Device
            };
            if (device != null)
            {
                audit.ItemId = device.Id;
                audit.DeviceGuid = device.Guid;
            }
            return audit;
        }

        internal void AuditRegisteringExistingDevice(Device device)
        {
            var audit = PrepareAudit(device);
            audit.Event = "Registered existing device";

            context.Audits.Add(audit);
            context.SaveChanges();
        }

        internal void AuditRegisteringNewDevice(Device device)
        {
            var audit = PrepareAudit(device);
            audit.Event = "Registered new device";

            context.Audits.Add(audit);
            context.SaveChanges();
        }
    }
}
