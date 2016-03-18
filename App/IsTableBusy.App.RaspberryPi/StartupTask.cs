using System.Threading.Tasks;
using IsTableBusy.App.RaspberryPi.Plugins;
using IsTableBusy.Device.Core.Logic;


namespace IsTableBusy.App.RaspberryPi
{
    public sealed class StartupTask : Windows.ApplicationModel.Background.IBackgroundTask
    {
        private Windows.ApplicationModel.Background.BackgroundTaskDeferral deferral;
        public void Run(Windows.ApplicationModel.Background.IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();

            var app = PrepareApp();

            app.Run();
        }

        private static DeviceApp PrepareApp()
        {
            var device = new RasbperryPi();
            var configStorageImp = new ConfigStorageImp("IsTableBusy.Config.xml");
            var config = new Config(configStorageImp);
            var apiClient = new ApiClientImp(config);
            var app = new DeviceApp(device, apiClient);
            return app;
        }
    }
}
