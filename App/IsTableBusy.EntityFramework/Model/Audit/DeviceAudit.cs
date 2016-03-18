using System;

namespace IsTableBusy.EntityFramework.Model.Audit
{
    public class DeviceAudit : BaseAudit
    {
        public Guid DeviceGuid { get; set; }
    }
}