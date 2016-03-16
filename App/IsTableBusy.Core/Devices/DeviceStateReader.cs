using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTableBusy.EntityFramework;

namespace IsTableBusy.Core.Devices
{
    public class DeviceStateReader
    {
        private Context context;

        public DeviceStateReader(Context context)
        {
            this.context = context;
        }

        public bool Read(Guid guid)
        {
            var table = context.Tables.Single(x => x.Device.Guid == guid);
            return table.IsBusy;
        }
    }
}
