using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration
{
    public class TableManagerTest : IsTableBusyDatabaseTest, IDisposable
    {
        [Fact]
        public void SetBusy()
        {
            using (Context ctx = new Context())
            {
                var table = new Table {Place = new Place {Name = "place1"}, Name = "table11", IsBusy = false};
                ctx.Tables.Add(table);
                ctx.SaveChanges();

                var testTable = ctx.Tables.First();
                testTable.IsBusy = false;
                ctx.SaveChanges();

                TableManager tm = new TableManager(ctx);
                tm.SetBusy(testTable.Id);

                var result = ctx.Tables.Single(x => x.Id == testTable.Id);
                result.IsBusy.Should().BeTrue();
            }
        }

        [Fact]
        public void SetFree()
        {
            using (Context ctx = new Context())
            {
                var table = new Table { Place = new Place { Name = "place1" }, Name = "table11", IsBusy = false };
                ctx.Tables.Add(table);
                ctx.SaveChanges();

                var testTable = ctx.Tables.First();
                testTable.IsBusy = true;
                ctx.SaveChanges();

                TableManager tm = new TableManager(ctx);
                tm.SetFree(testTable.Id);

                var result = ctx.Tables.Single(x => x.Id == testTable.Id);
                result.IsBusy.Should().BeFalse();
            }
        }
    }
}
