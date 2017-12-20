using System.Data.Entity;
using System.Linq;
using IsTableBusy.EntityFramework.Migrations;
using Tazos.Tools.XUnit;

namespace IsTableBusy.EntityFramework
{
    public class IsTabeBusyDatabaseTool: DatabaseCreator, DatabaseRemover
    {

        

        public void Create(string connectionString)
        {
         
                Context.ConnectionString = connectionString;
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
                using (var ctx = new Context())
                {
                    ctx.Places.Take(1).ToList();
                    ctx.SaveChanges();
                }
            
        }

        public void Remove(string connectionString)
        {
            Database.Delete(connectionString);
        }
    }
}