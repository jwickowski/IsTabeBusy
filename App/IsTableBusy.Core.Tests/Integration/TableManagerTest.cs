using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.Core.Tests.LoadData;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration
{
    public class TableManagerTest : IsTableBusyDatabaseTest, IDisposable
    {
        [Fact]
        public void SetBusy()
        {
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();

                var testTable = context.Tables.First(x=> x.Id == loadedData.ConnectedDevice.Id);
                testTable.IsBusy = false;
                context.SaveChanges();

                TableManager tm = new TableManager(context);
                tm.SetBusy(testTable.Id);

                var result = context.Tables.Single(x => x.Id == testTable.Id);
                result.IsBusy.Should().BeTrue();
        }

        [Fact]
        public void SetFree()
        {
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();

                var testTable = context.Tables.First(x => x.Id == loadedData.ConnectedDevice.Id);
                testTable.IsBusy = true;
                context.SaveChanges();

                TableManager tm = new TableManager(context);
                tm.SetFree(testTable.Id);

                var result = context.Tables.Single(x => x.Id == testTable.Id);
                result.IsBusy.Should().BeFalse();
        }
    }
}
