﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;

namespace IsTableBusy.Core.Tests.LoadData
{
    public class DeviceDrivenTestDataLoader
    {
        private Context context;

        public DeviceDrivenTestDataLoader(Context context)
        {
            this.context = context;
        }

        public DeviceDrivenTestData Load()
        {
            var result = new DeviceDrivenTestData();
            var place = new Place { Name = "DeviceDrivenPlace" };
            result.Place = place;
            this.context.Places.Add(place);

            var tableWithFreeDevice = new Table
            {
                Device = new Device { Guid = Guid.NewGuid() },
                IsBusy = false,
                Name = "TableWithFreeDevice",
                Place = place
            };
            result.TableWithFreeDevice = tableWithFreeDevice;
            this.context.Tables.Add(tableWithFreeDevice);

            var tableWithBusyDevice = new Table
            {
                Device = new Device { Guid = Guid.NewGuid() },
                IsBusy = true,
                Name = "TableWithBusyDevice",
                Place = place
            };
            result.TableWithBusyDevice = tableWithBusyDevice;
            this.context.Tables.Add(tableWithBusyDevice);

            var notConnectedDevice = new Device { Guid = Guid.NewGuid() };
            result.NotConnectedDevice = notConnectedDevice;
            this.context.Devices.Add(notConnectedDevice);

            this.context.SaveChanges();
            return result;
        }
    }
}
