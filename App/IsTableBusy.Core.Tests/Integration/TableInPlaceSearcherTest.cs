using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.EntityFramework;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration
{
    public class TableInPlaceSearcherTest: IsTableBusyDatabaseTest, IDisposable
    {
        [Fact]
        public void Read_should_get_5_items()
        {
            using (Context ctx = new Context())
            {
                TablesInPlaceReader reader = new TablesInPlaceReader(ctx);

                var result = reader.Read("place1");
                result.Should().NotBeEmpty();
                result.Count().Should().Be(5);
            }
        }
    }
}
