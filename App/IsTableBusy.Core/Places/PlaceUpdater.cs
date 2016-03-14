﻿using System.Linq;
using IsTableBusy.Core.Models;
using IsTableBusy.EntityFramework;

namespace IsTableBusy.Core.Places
{
    public class PlaceUpdater
    {
        private Context context;

        public PlaceUpdater(Context context)
        {
            this.context = context;
        }

        public void Update(PlaceViewModel place)
        {
            var item = context.Places.Single(x => x.Id == place.Id);
            item.Name = place.Name;
            this.context.SaveChanges();
        }
    }
}