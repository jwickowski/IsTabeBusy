using IsTableBusy.Core.Models;
using IsTableBusy.EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTableBusy.Core.Mappers
{
    public static class TableMapper
    {
        public static TableViewModel ToTableViewModel(this Table table)
        {
            return new TableViewModel { Id = table.Id, IsBusy = table.IsBusy, Name = table.Name };
        }
    }
}
