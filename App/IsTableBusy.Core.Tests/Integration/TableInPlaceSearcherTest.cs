using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.Core.Tests.LoadData;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration
{
    public class TableInPlaceSearcherTest : IsTableBusyDatabaseTest, IDisposable
    {
        [Fact]
        public void Empty()
        {
            TablesInPlaceReader reader = new TablesInPlaceReader(context);

            var result = reader.Read("place");
            result.Should().BeEmpty();
        }

        [Fact]
        public void return_item()
        {
            var loader = new StandardTestDataLoader(context);
            var loadedData = loader.Load();

            TablesInPlaceReader reader = new TablesInPlaceReader(context);

            var result = reader.Read(loadedData.PlaceWithTwoTables.Name);
            result.Should().NotBeEmpty();
            result.Count().Should().Be(2);

            var returnedItem = result.First();
            var orginalData = loadedData.PlaceWithTwoTables.Tables.Single(x => x.Id == returnedItem.Id);

            returnedItem.IsBusy.Should().Be(orginalData.IsBusy);
            returnedItem.Name.Should().Be(orginalData.Name);
            returnedItem.LastChangeStateDate.Should().Be(orginalData.LastChangeStateDate);
        }
    }
}