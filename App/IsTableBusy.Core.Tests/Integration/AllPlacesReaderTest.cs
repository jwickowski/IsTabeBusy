using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IsTableBusy.Core.Tests.LoadData;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration
{
    public class AllPlacesReaderTest : IsTableBusyDatabaseTest, IDisposable
    {

        [Fact]
        public void empty_table()
        {
            using (var context = new Context())
            {
                var reader = new AllPlacesReader(context);
                var result = reader.Read();
                result.Should().BeEmpty();
            }
        }

        [Fact]
        public void return_all()
        {
            using (var context = new Context())
            {
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();
                
                var reader = new AllPlacesReader(context);
                var result = reader.Read();

                result.Count().Should().Be(loadedData.AllPlaces().Count());
            }
        }
    }
}
