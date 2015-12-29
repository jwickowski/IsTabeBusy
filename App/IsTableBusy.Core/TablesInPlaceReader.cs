using IsTableBusy.Core.Mappers;
using IsTableBusy.Core.Models;
using IsTableBusy.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace IsTableBusy.Core
{
    public class TablesInPlaceReader
    {
        readonly Context context;
        public TablesInPlaceReader(Context context)
        {
            this.context = context;
        }

        public IEnumerable<TableViewModel> Read(string placeName)
        {
            var result = context
                .Tables
                .Where(x => x.Place.Name == placeName)
                .ToList()
                .Select(x => x.ToTableViewModel());

            return result;
        }
    }
}
