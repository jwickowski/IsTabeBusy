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
        private Func<bool> getBusy = () =>  false;
        public Action<bool> setBusy = (isBusy) => { };
        public Action registerDevice = () => { };

        public ApiClientFake WithGetBusy(Func<bool> getBusyFunction) {
            getBusy = getBusyFunction;
            return this;
        }

        public ApiClientFake WithSetBusy(Action<bool> setBusyFunction)
        {
            setBusy = setBusyFunction;
            return this;
        }

        internal ApiClientFake WithRegisterDevice(Action registerDeviceFunction)
        {
            registerDevice = registerDeviceFunction;
            return this;
        }

        public bool GetBusy()
        {
            return getBusy();
        }

        public void SetBusy(bool isBusy)
        {
            setBusy(isBusy);
        }
        public void RegisterDevice()
        {
            registerDevice();
        }
    }
}
