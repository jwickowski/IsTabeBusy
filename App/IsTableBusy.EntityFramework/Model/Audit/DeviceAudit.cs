using System;
using IsTableBusy.EntityFramework.Model.Audit;

namespace IsTableBusy.EntityFramework.Model
{
    public class DeviceAudit : BaseAudit
    {
        public Guid DeviceGuid { get; set; }
    }
}