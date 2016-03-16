using System;
using System.Linq;
using IsTableBusy.Core.Exceptions;
using IsTableBusy.Core.Places;
using IsTableBusy.EntityFramework;

namespace IsTableBusy.Core.Devices
{
    public class DeviceStateChanger
    {
        private Context context;
        private DeviceStateChangeAuditer auditer;
        private PlacesHubWrapper hub;

        public DeviceStateChanger(Context context, PlacesHubWrapper hub)
        {
            this.context = context;
            this.hub = hub;
            this.auditer = new DeviceStateChangeAuditer(this.context);
        }

        public void SetBusy(Guid guid, bool isBusy)
        {
            var table = context.Tables.SingleOrDefault(x => x.Device.Guid == guid);
            if (table == null)
            {
                throw new ChangingDeviceStateException();
            }
            table.IsBusy = isBusy;
            this.auditer.Audit(table);
            context.SaveChanges();
            if (table.IsBusy)
            {
                hub.IsBusy(table.Id);
            }
            else
            {
                hub.IsFree(table.Id);
            }
            
        }

    }
}
