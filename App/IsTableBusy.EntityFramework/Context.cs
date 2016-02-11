using IsTableBusy.EntityFramework.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace IsTableBusy.EntityFramework
{
    public class Context : DbContext
    {
        internal static string ConnectionString { get; set; } = "DefaultConnection";

        public Context() :  base(ConnectionString)
        {
          
        }   

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Place> Places { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Device> Devices { get; set; }
    }
}
