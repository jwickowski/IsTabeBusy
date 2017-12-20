using System.Collections.Generic;
using IsTableBusy.EntityFramework.Model;

namespace IsTableBusy.Core.Tests.LoadData
{
    public static class StandarsTestDataExtensions
    {
        public static IEnumerable<Place> AllPlaces(this StandardTestData @this)
        {
            return new List<Place>
            {
                @this.PlaceWithoutTable,
                @this.PlaceWithTwoTables
            };
        } 
    }
}