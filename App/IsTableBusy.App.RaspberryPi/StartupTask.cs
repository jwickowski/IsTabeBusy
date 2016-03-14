

using IsTableBusy.App.RaspberryPi.Logic;
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
            var config = new Config();
            var apiClient = new ApiClientImp(config);
            var lightManager = new LightManager(device);
            var app = new DeviceApp(device, apiClient);
            return app;
        }
    }
}
