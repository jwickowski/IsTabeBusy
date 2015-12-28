using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var testTable = ctx.Tables.First();
                testTable.IsBusy = false;
                ctx.SaveChanges();

                TableManager tm  = new TableManager(ctx);
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
