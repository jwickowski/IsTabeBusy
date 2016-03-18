using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using IsTableBusy.App.RaspberryPi.Logic;
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
