using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.Core.Tests.LoadData;
using IsTableBusy.EntityFramework;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration
{
    public class TableInPlaceSearcherTest : IsTableBusyDatabaseTest, IDisposable
    {
        [Fact]
        public void Empty()
        {
            using (Context ctx = new Context())
            {
                TablesInPlaceReader reader = new TablesInPlaceReader(ctx);

                var result = reader.Read("place");
                result.Should().BeEmpty();
            }
        }

        [Fact]
        public void return_item()
        {
            using (Context ctx = new Context())
            {
                var loader = new StandardTestDataLoader(ctx);
                var loadedData = loader.Load();

                TablesInPlaceReader reader = new TablesInPlaceReader(ctx);

                var result = reader.Read(loadedData.PlaceWithTwoTables.Name);
                result.Should().NotBeEmpty();
                result.Count().Should().Be(2);
            }
        }
    }
}