using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;
using IsTableBusy.EntityFramework.Model.Audit;

namespace IsTableBusy.Core.Devices
{
    public class DeviceStateChangeAuditer
    {
        private Context context;

        public DeviceStateChangeAuditer(Context context)
        {
            this.context = context;
        }

        public void Audit(Table table)
        {
            TableAudit audit = new TableAudit
            {
                Date = DateTimeSupplier.Date,
                Event = "State changed",
                ItemId = table.Id,
                ItemType = AuditItemType.Table,
                NewState = table.IsBusy
            };
            context.Audits.Add(audit);
        }
    }
}
