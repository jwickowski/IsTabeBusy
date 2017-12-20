using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using IsTableBusy.Device.Core.Logic;

namespace IsTableBusy.App.Symulator.Plugins
{
    public sealed class Led: Light
    {
        private readonly Ellipse _ellipse;
        private Brush baseFill;


        public Led(Ellipse ellipse)
        {
            _ellipse = ellipse;
            baseFill = ellipse.Fill;
        }

        public void On()
        {
            if (IsOn == false)
            {
                _ellipse.Fill = baseFill;
            }

        }

        public void Off()
        {
            if (IsOn)
            {
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 200);
                _ellipse.Fill = mySolidColorBrush;
            }
        }

        public bool IsOn => _ellipse.Fill == baseFill;
    }
}
