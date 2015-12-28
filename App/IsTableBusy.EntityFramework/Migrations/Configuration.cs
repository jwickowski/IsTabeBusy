namespace IsTableBusy.EntityFramework.Migrations
{
    using Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IsTableBusy.EntityFramework.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IsTableBusy.EntityFramework.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            for (var placeId = 1; placeId <=2; placeId++)
            {

                context.Places.AddOrUpdate(
                    x => x.Id, 
                    new Place { Id = placeId, Name = $"Place{placeId}" });

                for (var tableId = 1; tableId<=5; tableId++)
                {
                    context.Tables.AddOrUpdate(x => x.Id,
                        new Table() { Id = tableId, Name = $"Table{placeId}-{tableId}", IsBusy = false, PlaceId = placeId });
                }
            }

            //    context.SaveChanges();

            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
