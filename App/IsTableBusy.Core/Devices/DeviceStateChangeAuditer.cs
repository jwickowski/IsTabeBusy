using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;
using IsTableBusy.EntityFramework.Model.Audit;

namespace IsTableBusy.Core.Devices
{
    internal class DeviceStateChangeAuditer
    {
        private Context context;

        internal DeviceStateChangeAuditer(Context context)
        {
            this.context = context;
        }

        internal void Audit(Table table)
        {
            TableAudit audit = new TableAudit
            {
                Date = DateTimeSupplier.Date,
                Event = "State changed",
                ItemId = table.Id,
                ItemType = AuditItemType.Table,
                NewState = table.IsBusy,
                DeviceId = table.DeviceId                
            };
            context.Audits.Add(audit);
        }
    }
}
