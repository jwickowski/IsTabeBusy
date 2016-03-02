using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;

namespace IsTableBusy.Core
{
    public class DeviceTableConnector
    {
        private Context ctx;

        public DeviceTableConnector(Context ctx)
        {
            this.ctx = ctx;
        }

        public void Connect(int tableId, int deviceId)
        {
            
        }
    }
}
