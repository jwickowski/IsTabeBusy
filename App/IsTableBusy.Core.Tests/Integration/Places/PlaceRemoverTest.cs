using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.Core.Places;
using IsTableBusy.Core.Tests.LoadData;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration.Places
{
    public class PlaceRemoverTest : IsTableBusyDatabaseTest, IDisposable
    {

        [Fact]
        public void remove()
        {
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();
                var remover = new PlaceRemover(context);

                var itemId = loadedData.PlaceWithoutTable.Id;
                remover.Remove(itemId);

                var exists = context.Places.Any(x => x.Id == itemId);

                exists.Should().BeFalse();
        }
    }
}
