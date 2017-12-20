using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using IsTableBusy.App.Symulator.Plugins;
using IsTableBusy.Device.Core.Logic;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IsTableBusy.App.Symulator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }

        private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            var app = PrepareApp();
            app.Run();
        }

        private  DeviceApp PrepareApp()
        {
            var device = new FakeDevice(MainButton, RedEllypse, GreenEllypse);
            var configStorageImp = new ConfigStorageInMemory();
            var config = new Config(configStorageImp);
            var apiClient = new ApiClientImp(config);
            var app = new DeviceApp(device, apiClient);
            return app;
        }
    }
}
