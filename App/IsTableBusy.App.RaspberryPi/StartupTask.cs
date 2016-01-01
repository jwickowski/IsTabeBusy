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
            Device device = new RasbperryPi();
            var app = new Logic.App(device);
            app.Run();
        }
    }
}
