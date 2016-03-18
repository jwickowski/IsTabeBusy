using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.Core.Places;
using IsTableBusy.Core.Tests.LoadData;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration.Places
{
    public class AllPlacesReaderTest : IsTableBusyDatabaseTest, IDisposable
    {

        [Fact]
        public void empty_table()
        {
                var reader = new AllPlacesReader(context);
                var result = reader.Read();
                result.Should().BeEmpty();
        }

        [Fact]
        public void return_all()
        {
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();
                
                var reader = new AllPlacesReader(context);
                var result = reader.Read();

                result.Count().Should().Be(loadedData.AllPlaces().Count());
        }
    }
}
