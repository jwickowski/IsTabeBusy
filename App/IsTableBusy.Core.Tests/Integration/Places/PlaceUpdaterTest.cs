using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.Core.Models;
using IsTableBusy.Core.Places;
using IsTableBusy.Core.Tests.LoadData;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration.Places
{
   public  class PlaceUpdaterTest : IsTableBusyDatabaseTest, IDisposable
    {
        [Fact]
        public void update()
        {
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();
                var updater = new PlaceUpdater(context);

                var item = new PlaceViewModel {Id = loadedData.PlaceWithoutTable.Id, Name = "newPlaceName" };
                updater.Update(item);

                var itemFromDb = context.Places.Single(x=> x.Id == item.Id);

                itemFromDb
                    .ShouldBeEquivalentTo(item, options => options
                    .Including(x => x.Name)
                    .Including(x => x.Id));
        }
    }
}
