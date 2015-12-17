using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTableBusy.EntityFramework.Model
{
    public class Table :BaseEntity
    {
        public string Name { get; set; }

        public bool IsBusy { get; set; }

        public int PlaceId { get; set; }

        public Place Place { get; set; }
    }
}
