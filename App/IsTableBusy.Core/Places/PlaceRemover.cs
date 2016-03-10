using System.Linq;
using IsTableBusy.EntityFramework;

namespace IsTableBusy.Core.Places
{
    public class PlaceRemover
    {
        private Context context;

        public PlaceRemover(Context context)
        {
            this.context = context;
        }

        public void Remove(int id)
        {
            var item = context.Places.Single(x => x.Id == id);
            this.context.Places.Remove(item);
            this.context.SaveChanges();
        }
    }
}
