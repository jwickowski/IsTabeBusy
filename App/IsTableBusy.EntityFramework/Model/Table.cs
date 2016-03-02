using System.Data.Entity.Core.Metadata.Edm;

namespace IsTableBusy.EntityFramework.Model
{
    public class Table :BaseEntity
    {
        public string Name { get; set; }

        public bool IsBusy { get; set; }

        public int PlaceId { get; set; }
        public Place Place { get; set; }
        
        public int? DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
