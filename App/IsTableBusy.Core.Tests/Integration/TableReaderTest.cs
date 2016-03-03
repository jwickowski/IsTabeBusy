using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.Core.Models;
using IsTableBusy.Core.Tests.LoadData;
using IsTableBusy.EntityFramework;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration
{
    public class TableReaderTest : IsTableBusyDatabaseTest, IDisposable
    {
        [Fact]
        public void Read_table_correctly()
        {
            using (Context ctx = new Context())
            {
                var loader = new StandardTestDataLoader(ctx);
                var loadedData = loader.Load();

                var tableId = loadedData.PlaceWithTwoTables.Tables.First().Id;

                var reader = new TableReader(ctx);

                TableViewModel result = reader.Read(loadedData.PlaceWithTwoTables.Name, tableId);
                result.Should().NotBeNull();
                result.Id.Should().Be(tableId);
            }
        }
    }
}
