using System;
using System.Web.Http;
using IsTableBusy.Core;
using IsTableBusy.Core.Devices;
using IsTableBusy.Core.Models;

namespace IsTableBusy.App.Api.Controllers
{
    public class DevicesController : ApiController
    {
        private readonly DeviceRegister deviceRegister;
        private readonly DeviceStateChanger deviceStateChanger;

        public DevicesController(DeviceRegister deviceRegister, DeviceStateChanger deviceStateChanger)
        {
            this.deviceRegister = deviceRegister;
            this.deviceStateChanger = deviceStateChanger;
        }

        [HttpPost]
        [Route("devices/register/{guid:Guid}")]
        public DeviceRegisterViewModel Register(Guid guid)
        {
            var result  = this.deviceRegister.Register(guid);
            return new DeviceRegisterViewModel {Guid = result};
        }

        [HttpPost]
        [Route("devices/register")]
        public DeviceRegisterViewModel Register()
        {
            var result = this.deviceRegister.Register();
            return new DeviceRegisterViewModel { Guid = result };
        }

        [HttpPost]
        [Route("devices/{guid:Guid}/SetBusy")]
        public void ChangeStateToBusy(Guid guid)
        {
            this.deviceStateChanger.SetBusy(guid, true);

        }
        [HttpPost]
        [Route("devices/{guid:Guid}/SetFree")]
        public void ChangeStateToFree(Guid guid)
        {
            this.deviceStateChanger.SetBusy(guid, false);
        }
    }
}
