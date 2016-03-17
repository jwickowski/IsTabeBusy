
using IsTableBusy.Device.Core.Logic;
using System;
using Windows.Foundation;

namespace IsTableBusy.App.RaspberryPi.Logic
{
    public sealed class ButtonImp : Button
    {
        public ButtonImp(Windows.UI.Xaml.Controls.Button button)
        {
            button.Click += Button_Click;
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Clicked?.Invoke(this, null);
        }

        public event TypedEventHandler<Button, object> Clicked;
    }
}
