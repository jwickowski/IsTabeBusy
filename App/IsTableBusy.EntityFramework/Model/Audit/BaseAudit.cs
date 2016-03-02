using System;

namespace IsTableBusy.EntityFramework.Model.Audit
{
    public abstract class BaseAudit : BaseEntity
    {
        public DateTime Date { get; set; }
        public AuditItemType ItemType { get; set; }
        public int? ItemId { get; set; }
    }
}
