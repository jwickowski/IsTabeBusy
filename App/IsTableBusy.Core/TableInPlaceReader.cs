using IsTableBusy.Core.Mappers;
using IsTableBusy.Core.Models;
using IsTableBusy.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace IsTableBusy.Core
{
    public class TableInPlaceReader
    {
        IEnumerable<TableViewModel> Read(string placeName)
        {
            using (var context = new Context())
            {
                var tablesQuery = context.Tables.Where(x => x.Place.Name == placeName);
                var result = tablesQuery.Select(x => x.ToTableViewModel());
                return result;
            }
        }
    }


}
