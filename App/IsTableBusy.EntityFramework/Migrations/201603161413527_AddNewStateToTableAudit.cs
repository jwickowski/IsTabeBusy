namespace IsTableBusy.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddNewStateToTableAudit : DbMigration
    {
        public override void Up()
        {
            AddColumn("Audit.TableAudit", "NewState", c => c.Boolean());
            AlterColumn("Audit.TableAudit", "DeviceId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("Audit.TableAudit", "DeviceId", c => c.Int(nullable: false));
            DropColumn("Audit.TableAudit", "NewState");
        }
    }
}
