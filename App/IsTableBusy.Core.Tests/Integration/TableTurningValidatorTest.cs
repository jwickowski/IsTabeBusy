using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.Core.Exceptions;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;
using Xunit;
using Tazos.Tools.Extensions.EntityFramework;

namespace IsTableBusy.Core.Tests.Integration
{
    public class TableTurningValidatorTest : IsTableBusyDatabaseTest, IDisposable
    {
        [Fact]
        public void TableInCorrectPlace()
        {
            using (Context ctx = new Context())
            {

                var table = new Table { Place = new Place { Name = "place1" }, Name = "table11", IsBusy = false };
                ctx.Tables.Add(table);
                ctx.SaveChanges();

                var testTable = ctx.Tables.Include(x => x.Place).First();

                var ttv = new TableTurningValidator(ctx);
                Action action = () => ttv.Validate(testTable.Place.Name, testTable.Id);
                action.ShouldNotThrow();
            }
        }

        [Fact]
        public void Table_in_wrong_place()
        {
            using (Context ctx = new Context())
            {
                var table = new Table { Place = new Place { Name = "place1" }, Name = "table11", IsBusy = false };
                ctx.Tables.Add(table);
                ctx.SaveChanges();
                var testTable = ctx.Tables.Include(x => x.Place).First();

                var ttv = new TableTurningValidator(ctx);
                Action action = () => ttv.Validate("wrongPlaceName", testTable.Id);
                action.ShouldThrow<TableInWrongPlaceException>();
            }
        }

    }
}
