using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTableBusy.Device.Core.Logic
{
   public  interface ApiClient
    {
        Table GetTable();

        void SetBusy(bool isBusy);

        Guid RegisterDevice();
    }
}
