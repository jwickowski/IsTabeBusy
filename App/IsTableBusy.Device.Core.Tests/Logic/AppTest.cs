using System;
using IsTableBusy.Device.Core.Logic;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace IsTableBusy.Device.Core.Tests.Logic
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

        [TestMethod]
        public void change_state_to_busy_on_click()
        {
            ChangeStateTest(before: true, after: false);
        }

        [TestMethod]
        public void change_state_to_free_on_click()
        {
            ChangeStateTest(before: false, after:true);
        }

        public void ChangeStateTest(bool before, bool after)
        {
            Init();
            bool isBusyRan = false;
            bool isBusyValue = false;

            apiClient
             .WithGetBusy(() => { return before; })
             .WithSetBusy((isBusy) =>
             {
                 isBusyRan = true;
                 isBusyValue = isBusy;
             });

            app.Run();

            device.FakeButton.RaiseEvent();

            Assert.AreEqual(true, isBusyRan);
            Assert.AreEqual(after, isBusyValue);
        }

        [TestMethod]
        public void change_state_and_device_is_not_connected()
        {
            Init();
            apiClient
             .WithGetBusy(() => { return true; })
             .WithSetBusy((isBusy) =>
             {
                 throw new Exception("Changing state error");
             });

            app.Run();
            device.FakeButton.RaiseEvent();
            Assert.AreEqual(AppState.Error, app.State);
        }
    }
}


