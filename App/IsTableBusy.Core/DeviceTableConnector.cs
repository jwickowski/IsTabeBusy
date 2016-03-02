using System.Linq;
using IsTableBusy.Core.Exceptions;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;
using IsTableBusy.EntityFramework.Model.Audit;

namespace IsTableBusy.Core
{
    public class DeviceTableConnector
    {
        private Context ctx;

        public DeviceTableConnector(Context ctx)
        {
            this.ctx = ctx;
        }

        public void Connect(int tableId, int deviceId)
        {
            var table = ctx.Tables.SingleOrDefault(x => x.Id == tableId);
            Validate(table, deviceId);

            table.DeviceId = deviceId;

            Audit(table);
            ctx.SaveChanges();
        }

        private void Audit(Table table)
        {
            var tableAudit = new TableAudit
            {
                Event = "Table connected with device",
                DeviceId = table.DeviceId.Value,
                Date = DateTimeSupplier.Date,
                ItemId = table.Id,
                ItemType = AuditItemType.Table
            };
            ctx.Audits.Add(tableAudit);
            ctx.SaveChanges();
        }

        private void Validate(Table table, int deviceId)
        {
            if (table == null)
            {
                throw new TableDeviceConnectingException();
            }

            if (table.DeviceId.HasValue && table.DeviceId.Value != deviceId)
            {
                throw new TableDeviceConnectingException();
            }

            var device = ctx.Devices.SingleOrDefault(x => x.Id == deviceId);
            if (device == null)
            {
                throw new TableDeviceConnectingException();
            }
        }
    }
}
