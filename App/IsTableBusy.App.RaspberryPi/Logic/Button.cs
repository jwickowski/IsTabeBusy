using Windows.Foundation;

namespace IsTableBusy.App.RaspberryPi.Logic
{
    public interface Button
    {
        event TypedEventHandler<ButtonImp, object> Clicked;
    }
}
