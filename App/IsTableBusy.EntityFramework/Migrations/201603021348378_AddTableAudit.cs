namespace IsTableBusy.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddTableAudit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Audit.TableAudit",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Event = c.String(),
                        DeviceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Audit.BaseAudit", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Audit.TableAudit", "Id", "Audit.BaseAudit");
            DropIndex("Audit.TableAudit", new[] { "Id" });
            DropTable("Audit.TableAudit");
        }
    }
}
