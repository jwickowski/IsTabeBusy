using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;
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
        public void return_one_item()
        {
            using (Context ctx = new Context())
            {
                TablesInPlaceReader reader = new TablesInPlaceReader(ctx);

                var table = new Table { Place = new Place { Name = "place1" }, Name = "table11", IsBusy = false };
                ctx.Tables.Add(table);
                ctx.SaveChanges();

                var result = reader.Read("place1");
                result.Should().NotBeEmpty();
                result.Count().Should().Be(1);
            }
        }
    }
}