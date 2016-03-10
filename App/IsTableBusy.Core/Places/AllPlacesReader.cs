using System.Collections.Generic;
using System.Linq;
using IsTableBusy.Core.Models;
using IsTableBusy.EntityFramework;

namespace IsTableBusy.Core
{
    public class AllPlacesReader
    {
        private Context context;

        public AllPlacesReader(Context context)
        {
            this.context = context;
        }

        public IEnumerable<PlaceViewModel> Read()
        {
            var result = context.Places.Select(x => new PlaceViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return result;
        }
    }
}
