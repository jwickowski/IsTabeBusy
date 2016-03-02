namespace IsTableBusy.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectTableWithDevice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tables", "DeviceId", c => c.Int());
            CreateIndex("dbo.Tables", "DeviceId");
            AddForeignKey("dbo.Tables", "DeviceId", "dbo.Devices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tables", "DeviceId", "dbo.Devices");
            DropIndex("dbo.Tables", new[] { "DeviceId" });
            DropColumn("dbo.Tables", "DeviceId");
        }
    }
}
