using System.Collections.Generic;
using IsTableBusy.EntityFramework.Model;

namespace IsTableBusy.Core.Tests.LoadData
{
    public static class LoadedDataExtensions
    {
        public static IEnumerable<Place> AllPlaces(this LoadedData @this)
        {
            return new List<Place>
            {
                @this.PlaceWithoutTable,
                @this.PlaceWithTwoTables
            };
        } 
    }
}