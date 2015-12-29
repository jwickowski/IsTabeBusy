using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IsTableBusy.Core.Exceptions;
using IsTableBusy.Core.Models;
using IsTableBusy.EntityFramework;
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

                var firstTable = ctx.Tables.Include(x=>x.Place).First();
                var reader = new TableReader(ctx);

                TableViewModel result = reader.Read(firstTable.Place.Name, firstTable.Id);
                result.Should().NotBeNull();
                result.Id.Should().Be(firstTable.Id);
            }
        }
    }
}
