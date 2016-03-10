using System;
using System.Web.Http;
using IsTableBusy.Core;
using IsTableBusy.Core.Models;

namespace IsTableBusy.App.Api.Controllers
{
    public class DevicesController : ApiController
    {
        private readonly DeviceRegister deviceRegister;

        public DevicesController(DeviceRegister deviceRegister)
        {
            this.deviceRegister = deviceRegister;
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

    }
}
