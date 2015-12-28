﻿using System;
using System.Linq;
using FluentAssertions;
using IsTableBusy.EntityFramework;
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
                var testTable = ctx.Tables.Include(x => x.Place).First();

                var ttv = new TableTurningValidator(ctx);
                Action action = () => ttv.Validate(testTable.Place.Name, testTable.Id);
                action.ShouldNotThrow();
                true.Should().BeTrue();
            }
        }
       
    }
}
