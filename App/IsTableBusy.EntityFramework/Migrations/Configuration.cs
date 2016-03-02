using System.Data.Entity.Migrations;

namespace IsTableBusy.EntityFramework.Migrations
{  
    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context context)
        {
         
        }
    }
}
