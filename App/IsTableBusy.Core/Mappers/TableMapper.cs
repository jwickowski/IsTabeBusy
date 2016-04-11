using IsTableBusy.Core.Models;
using IsTableBusy.EntityFramework.Model;

namespace IsTableBusy.Core.Mappers
{
    public static class TableMapper
    {
        public static TableViewModel ToTableViewModel(this Table table)
        {
            return new TableViewModel { Id = table.Id, IsBusy = table.IsBusy, Name = table.Name, LastChangeStateDate = table.LastChangeStateDate};
        }
    }
}
