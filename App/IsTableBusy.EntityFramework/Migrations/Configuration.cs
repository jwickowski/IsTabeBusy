using System.Data.Entity.Migrations;

namespace IsTableBusy.EntityFramework.Migrations
{  
    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Context context)
        {
         
        }
    }
}
