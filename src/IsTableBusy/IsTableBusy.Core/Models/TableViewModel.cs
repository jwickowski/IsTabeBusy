using System;

namespace IsTableBusy.Core.Models
{
    public class TableViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsBusy { get; set; }

        public DateTime LastChangeStateDate { get; set; }
    }
}
