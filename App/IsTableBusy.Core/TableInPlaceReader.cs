using IsTableBusy.Core.Models;
using System.Collections.Generic;

namespace IsTableBusy.Core
{
    public interface TableInPlaceReader
    {
        IEnumerable<Table> Read(string placeName);
    }
}
