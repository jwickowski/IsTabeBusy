
using IsTableBusy.Device.Core.Logic;

namespace IsTableBusy.App.RaspberryPi.Logic
{
    public interface Button
    {
        event System.EventHandler<ButtonClickedEventArgs> Clicked;
        
    }
}
