using System.Linq;
using IsTableBusy.EntityFramework;

namespace IsTableBusy.Core
{
    public class TableManager
    {
        private Context context;

        public TableManager(Context context)
        {
            this.context = context;
        }

        public void SetBusy(int tableId)
        {
            ChangeIsBusy(tableId, true);
        }

        public void SetFree(int tableId)
        {
            ChangeIsBusy(tableId, false);
        }

        private void ChangeIsBusy(int tableId, bool isBusy)
        {
            var table = context.Tables
              .Single(x => x.Id == tableId);

            table.IsBusy = isBusy;

            context.SaveChanges();
        }
    }
}
