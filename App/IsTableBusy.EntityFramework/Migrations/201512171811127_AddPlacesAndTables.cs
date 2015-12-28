namespace IsTableBusy.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddPlacesAndTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsBusy = c.Boolean(nullable: false),
                        PlaceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId)
                .Index(t => t.PlaceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tables", "PlaceId", "dbo.Places");
            DropIndex("dbo.Tables", new[] { "PlaceId" });
            DropTable("dbo.Tables");
            DropTable("dbo.Places");
        }
    }
}
