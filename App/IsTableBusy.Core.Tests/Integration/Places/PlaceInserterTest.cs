using System;
using System.Linq;
using FluentAssertions;
using FluentValidation;
using IsTableBusy.Core.Models;
using IsTableBusy.Core.Places;
using IsTableBusy.Core.Tests.LoadData;
using IsTableBusy.EntityFramework;
using Xunit;

namespace IsTableBusy.Core.Tests.Integration.Places
{
    public class PlaceInserterTest : IsTableBusyDatabaseTest, IDisposable
    {
        [Fact]
        public void insert()
        {
            using (var context = new Context())
            {
                PlaceInserter inserter = new PlaceInserter(context);
                var item = new PlaceViewModel { Name = "testPlace" };
                inserter.Insert(item);

                var places = context.Places.ToList();
                places.Should().HaveCount(1);
                var itemFromDb = places.Single();

                itemFromDb
                    .ShouldBeEquivalentTo(item, options => options
                    .Including(x => x.Name)
                    .Including(x => x.Id));
            }
        }

        [Fact]
        public void trim_name()
        {
            using (var context = new Context())
            {
                PlaceInserter inserter = new PlaceInserter(context);
                var item = new PlaceViewModel { Name = "  testPlace  " };
                inserter.Insert(item);

                var place = context.Places.Single();
                place.Name.Should().Be("testPlace");
                item.Name.Should().Be("testPlace");
            }
        }

        [Fact]
        public void valide_unique_name()
        {
            using (var context = new Context())
            {
                var loader = new StandardTestDataLoader(context);
                var loadedData = loader.Load();

                PlaceInserter inserter = new PlaceInserter(context);
                var item = new PlaceViewModel { Name = loadedData.PlaceWithoutTable.Name };
                Action a = () => { inserter.Insert(item); };

                a.ShouldThrow<ValidationException>().Where(x => x.Message.Contains("Name must be unique"));
            }
        }

        [Fact]
        public void name_cannot_be_null_whitespace()
        {
            using (var context = new Context())
            {
                PlaceInserter inserter = new PlaceInserter(context);
                var item = new PlaceViewModel();

                Action a = () => { inserter.Insert(item); };

                item.Name = null;
                a.ShouldThrow<ValidationException>();

                item.Name = "";
                a.ShouldThrow<ValidationException>();

                item.Name = "   ";
                a.ShouldThrow<ValidationException>();
            }
        }
    }
}