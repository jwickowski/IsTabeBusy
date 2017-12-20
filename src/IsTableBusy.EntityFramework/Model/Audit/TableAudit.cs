namespace IsTableBusy.EntityFramework.Model.Audit
{
    public class TableAudit :BaseAudit
    {
        public int? DeviceId { get; set; }
        public bool? NewState { get; set; }
    }
}