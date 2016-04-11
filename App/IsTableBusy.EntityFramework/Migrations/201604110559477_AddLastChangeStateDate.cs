using Tazos.Tools.Extensions.EntityFramework;

namespace IsTableBusy.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLastChangeStateDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tables", "LastChangeStateDate", c => c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"));
            Sql(DbMigrationHelpers.DropDefaultConstraintSql("dbo.Tables", "LastChangeStateDate"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tables", "LastChangeStateDate");
        }
    }
}
