using Windows.ApplicationModel.Background;

namespace IsTableBusy.App.RaspberryPi
{
    public sealed class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();
            var app = new App();
            app.Run();
        }
    }
}
