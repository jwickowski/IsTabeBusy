using Windows.Foundation;

namespace IsTableBusy.Device.Core.Logic
{
    public interface Button
    {
        event TypedEventHandler<Button, object> Clicked;
    }
}
