using System;
using System.Web.Http;
using IsTableBusy.Core.Devices;
using IsTableBusy.Core.Models;
using IsTableBusy.App.Api.Models;

namespace IsTableBusy.App.Api.Controllers
{
    public class DevicesController : ApiController
    {
        private readonly DeviceRegister deviceRegister;
        private readonly DeviceStateChanger deviceStateChanger;
        private readonly DeviceStateReader deviceStateReader;

        public DevicesController(
            DeviceRegister deviceRegister,
            DeviceStateChanger deviceStateChanger,
            DeviceStateReader deviceStateReader)
        {
            this.deviceRegister = deviceRegister;
            this.deviceStateChanger = deviceStateChanger;
            this.deviceStateReader = deviceStateReader;
        }

        [HttpPost]
        [Route("devices/register")]
        public DeviceRegisterViewModel Register()
        {
            var result = this.deviceRegister.Register();
            return new DeviceRegisterViewModel { Guid = result };
        }

        [HttpPost]
        [Route("devices/register/{guid:Guid}")]
        public DeviceRegisterViewModel Register(Guid guid)
        {
            var result = this.deviceRegister.Register(guid);
            return new DeviceRegisterViewModel { Guid = result };
        }
        
        [HttpGet]
        [Route("devices/{guid:Guid}/State")]
        public DeviceStateViewModel GetState(Guid guid)
        {
            var result = new DeviceStateViewModel { IsBusy = deviceStateReader.Read(guid) };
            return result;
        }

        [HttpPost]
        [Route("devices/{guid:Guid}/State")]
        public void ChangeStateToBusy(Guid guid, DeviceStateViewModel deviceState)
        {
            this.deviceStateChanger.SetBusy(guid, deviceState.IsBusy);

        }
    }
}
