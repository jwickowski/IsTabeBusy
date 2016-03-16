using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTableBusy.EntityFramework;

namespace IsTableBusy.Core.Devices
{
    public class DeviceStateChanger
    {
        private Context context;

        public DeviceStateChanger(Context context)
        {
            this.context = context;
        }

        public void SetBusy(Guid guid, bool isBusy)
        {
            var table = context.Tables.SingleOrDefault(x => x.Device.Guid == guid);
            table.IsBusy = isBusy;
            context.SaveChanges();
        }

    }
}
