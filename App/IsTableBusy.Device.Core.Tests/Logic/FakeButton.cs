using Windows.Foundation;
using IsTableBusy.Device.Core.Logic;

namespace IsTableBusy.Device.Core.Tests.Logic
{
    public sealed class FakeButton : Button
    {
        public void RaiseEvent()
        {
            if (Clicked != null)
            {
                this.Clicked(this, null);
            }
        }

        public event TypedEventHandler<Button, object> Clicked;
    }
}