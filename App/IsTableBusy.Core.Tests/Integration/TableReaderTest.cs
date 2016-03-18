﻿using System;
using FluentAssertions;
using IsTableBusy.Core.Models;
using IsTableBusy.Core.Tests.LoadData;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration
{
    public class TableReaderTest : IsTableBusyDatabaseTest, IDisposable
    {
        [Fact]
        public void Read_table_correctly()
        {
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();

                var tableId = loadedData.TableWithDevice.Id;
                var placeName = loadedData.TableWithDevice.Place.Name;

                var reader = new TableReader(context);

                TableViewModel result = reader.Read(placeName, tableId);
                result.Should().NotBeNull();
                result.Id.Should().Be(tableId);
        }
    }
}
