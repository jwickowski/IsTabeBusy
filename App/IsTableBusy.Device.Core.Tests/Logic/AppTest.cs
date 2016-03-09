


using IsTableBusy.Device.Core.Logic;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace IsTableBusy.App.RaspberryPi.Tests.Logic
{
    [TestClass]
    public class AppTest
    {
        [TestMethod]
        public void new_app_is_not_started()
        {
            IoTDevice device = new FakeDevice();
            var app = new DeviceApp(device, null);
            Assert.AreEqual(app.State,AppState.NotStarted);
        }
    }
}
