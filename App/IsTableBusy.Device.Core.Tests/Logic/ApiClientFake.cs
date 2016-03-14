using IsTableBusy.Device.Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTableBusy.Device.Core.Tests.Logic
{
    public class ApiClientFake : ApiClient
    {
        private Func<Table> getTable;
        public Action<bool> setBusy;
        public Func<Guid> registerDevice;

        public ApiClientFake WithGetTable(Func<Table> getTableFunction) {
            getTable = getTableFunction;
            return this;
        }

        public ApiClientFake WithSetBusy(Action<bool> setBusyFunction)
        {
            setBusy = setBusyFunction;
            return this;
        }

        internal ApiClientFake WithRegisterDevice(Func<Guid> registerDeviceFunction)
        {
            registerDevice = registerDeviceFunction;
            return this;
        }

        public Table GetTable()
        {
            return getTable();
        }

        public void SetBusy(bool isBusy)
        {
            setBusy(isBusy);
        }
        public Guid RegisterDevice()
        {
            return registerDevice();
        }
    }
}
