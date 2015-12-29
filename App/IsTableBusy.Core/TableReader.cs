
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTableBusy.Core.Mappers;
using IsTableBusy.Core.Models;
using IsTableBusy.EntityFramework;

namespace IsTableBusy.Core
{
    public class TableReader
    {
        private Context context;

        public TableReader(Context context)
        {
            this.context = context;
        }

        public TableViewModel Read(string palceName, int tableId)
        {
            var result  = context.Tables
                .Single(x => x.Id == tableId && x.Place.Name == palceName)
                .ToTableViewModel();
            return result;
        }
    }
}
