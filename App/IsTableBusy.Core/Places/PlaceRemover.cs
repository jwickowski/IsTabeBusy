using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTableBusy.Core.Models;
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
