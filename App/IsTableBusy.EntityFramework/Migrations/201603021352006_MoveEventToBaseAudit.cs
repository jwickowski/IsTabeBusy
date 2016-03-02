namespace IsTableBusy.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveEventToBaseAudit : DbMigration
    {
        public override void Up()
        {
            AddColumn("Audit.BaseAudit", "Event", c => c.String());
            DropColumn("Audit.TableAudit", "Event");
            DropColumn("Audit.DeviceAudit", "Event");
        }
        
        public override void Down()
        {
            AddColumn("Audit.DeviceAudit", "Event", c => c.String());
            AddColumn("Audit.TableAudit", "Event", c => c.String());
            DropColumn("Audit.BaseAudit", "Event");
        }
    }
}
