using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.Core.Tests.LoadData;
using IsTableBusy.EntityFramework;
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
                var loader = new StandardTestDataLoader(ctx);
                var loadedData = loader.Load();

                var testTable = ctx.Tables.First(x=> x.Id == loadedData.ConnectedDevice.Id);
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
                var loader = new StandardTestDataLoader(ctx);
                var loadedData = loader.Load();

                var testTable = ctx.Tables.First(x => x.Id == loadedData.ConnectedDevice.Id);
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
