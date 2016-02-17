using System.Reflection;
using Windows.ApplicationModel.Background;
using IsTableBusy.App.RaspberryPi.Logic;


namespace IsTableBusy.App.RaspberryPi
{
    public sealed class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();

            var app = PrepareApp();

            app.Run();
        }

        private static Logic.DeviceApp PrepareApp()
        {
            var device = new RasbperryPi();
            var config = new Config();
            var apiClient = new ApiClient(config);
            var lightManager = new LightManager(device);
            var app = new Logic.DeviceApp(device, apiClient, lightManager);
            return app;
        }
    }
}
