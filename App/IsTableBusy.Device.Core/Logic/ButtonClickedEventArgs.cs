using IsTableBusy.App.RaspberryPi.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTableBusy.Device.Core.Logic
{
    public class ButtonClickedEventArgs : EventArgs
    {
        public Button Button
        {
            get; set;
        }
    }
}
