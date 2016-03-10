namespace IsTableBusy.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class DeviceAudit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Audit.BaseAudit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ItemType = c.Int(nullable: false),
                        ItemId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Audit.DeviceAudit",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DeviceGuid = c.Guid(nullable: false),
                        Event = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Audit.BaseAudit", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Audit.DeviceAudit", "Id", "Audit.BaseAudit");
            DropIndex("Audit.DeviceAudit", new[] { "Id" });
            DropTable("Audit.DeviceAudit");
            DropTable("Audit.BaseAudit");
        }
    }
}
