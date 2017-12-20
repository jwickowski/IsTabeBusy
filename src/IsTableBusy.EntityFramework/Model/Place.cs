using System.Collections.Generic;

namespace IsTableBusy.EntityFramework.Model
{
    public class Place : BaseEntity
    {
        public string Name { get; set; }

        public IEnumerable<Table> Tables { get; set; }
    }
}
