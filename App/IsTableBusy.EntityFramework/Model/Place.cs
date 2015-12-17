using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTableBusy.EntityFramework.Model
{
    public class Place : BaseEntity
    {
        public string Name { get; set; }

        public IEnumerable<Table> Tables { get; set; }
    }
}
