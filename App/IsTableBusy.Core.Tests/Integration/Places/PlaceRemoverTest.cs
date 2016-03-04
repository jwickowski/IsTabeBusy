using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IsTableBusy.Core.Models;
using IsTableBusy.Core.Places;
using IsTableBusy.Core.Tests.LoadData;
using IsTableBusy.EntityFramework;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration.Places
{
    public class PlaceRemoverTest
    {

        [Fact]
        public void remove()
        {
            using (var context = new Context())
            {
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();
                var remover = new PlaceRemover(context);

                var itemId = loadedData.PlaceWithoutTable.Id;
                remover.Remove(itemId);

                var exists = context.Places.Any(x => x.Id == itemId);

                exists.Should().BeFalse();
            }
        }
    }
}
