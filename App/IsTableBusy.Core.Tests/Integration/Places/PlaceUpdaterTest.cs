using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IsTableBusy.Core.Models;
using IsTableBusy.Core.Places;
using IsTableBusy.Core.Tests.LoadData;
using IsTableBusy.EntityFramework;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration.Places
{
   public  class PlaceUpdaterTest
    {

        [Fact]
        public void update()
        {
            using (var context = new Context())
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
}
