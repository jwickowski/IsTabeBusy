using IsTableBusy.Device.Core.Logic;
using IsTableBusy.Device.Core.Tests.Logic;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;

namespace IsTableBusy.App.RaspberryPi.Tests.Logic
{
    [TestClass]
    public class AppTest
    {
        FakeDevice device;
        ApiClientFake apiClient;
        DeviceApp app;

        public void Init()
        {
            device = new FakeDevice();
            apiClient = new ApiClientFake();
            app = new DeviceApp(device, apiClient);
        }

        [TestMethod]
        public void new_app_is_not_started()
        {
            Init();
            Assert.AreEqual(app.State, AppState.NotStarted);
        }

        [TestMethod]
        public void register_app_on_start()
        {
            Init();
            bool registerExecuted = false;

            apiClient
                .WithRegisterDevice(() =>
                {
                    registerExecuted = true;
                });

            app.Run();

            Assert.IsTrue(registerExecuted);
        }

        [TestMethod]
        public void register_error()
        {
            Init();
            apiClient
                .WithRegisterDevice(() =>
                {
                    throw new Exception("Device registration error");
                });

            app.Run();

            Assert.AreEqual(AppState.Error, app.State);
        }

        [TestMethod]
        public void device_is_not_connected_with_table()
        {
            Init();
            apiClient
               .WithGetBusy(() =>
               {
                   throw new Exception("Reading table error");
               });
            app.Run();

            Assert.AreEqual(AppState.Error, app.State);
        }

        [TestMethod]
        public void try_run_again_when_state_is_error_on_click()
        {
            Init();
            apiClient
                .WithRegisterDevice(() =>
                {
                    throw new Exception("Device registration error");
                });

            app.Run();
            Assert.AreEqual(AppState.Error, app.State);

            var askForRegistration = false;
            var askForBusy = false;
            apiClient
             .WithRegisterDevice(() => { askForRegistration = true; })
             .WithGetBusy(() => { askForBusy = true; return true; });

            device.FakeButton.RaiseEvent();

            Assert.AreEqual(true, askForRegistration);
            Assert.AreEqual(true, askForBusy);
        }
    }
}


