using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IsTableBusy.Core.Exceptions;
using IsTableBusy.Core.Models;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;
using Xunit;
using Tazos.Tools.Extensions.EntityFramework;

namespace IsTableBusy.Core.Tests.Integration
{
    public class TableReaderTest : IsTableBusyDatabaseTest, IDisposable
    {
        [Fact]
        public void Read_table_correctly()
        {
            using (Context ctx = new Context())
            {
                var table = new Table { Place = new Place { Name = "place1" }, Name = "table11", IsBusy = false };
                ctx.Tables.Add(table);
                ctx.SaveChanges();

                var reader = new TableReader(ctx);

                TableViewModel result = reader.Read(table.Place.Name, table.Id);
                result.Should().NotBeNull();
                result.Id.Should().Be(table.Id);
            }
        }
    }
}
