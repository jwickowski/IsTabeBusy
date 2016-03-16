using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using IsTableBusy.Device.Core.Exceptions;
using Newtonsoft.Json;
using Windows.Storage;
using System.Text;

namespace IsTableBusy.Device.Core.Logic
{
    internal class DeviceViewModel
    {
        public Guid Guid { get; set; }
    }

    internal class DeviceStateViewModel
    {
        public bool IsBusy { get; set; }
    }

    public sealed class ApiClientImp : ApiClient
    {
        private readonly Config config;

        public ApiClientImp(Config config)
        {
            this.config = config;
        }

        public bool GetBusy()
        {
            try
            {
                HttpClient hc = new HttpClient();
                Uri baseUri = new Uri(config.ApiUrl);
                Uri tablesUri = new Uri(baseUri, $"api/devices/{config.DeviceGuid}/State");
                var responseTask = hc.GetStringAsync(tablesUri);
                var response = responseTask.Result;
                var state = JsonConvert.DeserializeObject<DeviceStateViewModel>(response);
                return state.IsBusy;
            }
            catch (AggregateException ex)
            {
                throw new Exception("Reading table error", ex);
            }
        }

        public void RegisterDevice()
        {
            try
            {
                HttpClient hc = new HttpClient();
                Uri baseUri = new Uri(config.ApiUrl);
                Uri registerUri = new Uri(baseUri, $"api/devices/register");
                var responseTask = hc.PostAsync(registerUri, null);
                var response = responseTask.Result;
                var data = response.Content.ReadAsStringAsync().Result;
                var deviceData = JsonConvert.DeserializeObject<DeviceViewModel>(data);
                config.DeviceGuid = deviceData.Guid;
            }
            catch (AggregateException)
            {
                throw new Exception("Device registration error");
            }
        }

        public void SetBusy(bool isBusy)
        {
            try
            {
                HttpClient hc = new HttpClient();
                Uri baseUri = new Uri(config.ApiUrl);
                Uri stateUri = new Uri(baseUri, $"api/devices/{config.DeviceGuid}/State");

                var data = new StringContent(JsonConvert.SerializeObject(new DeviceStateViewModel { IsBusy = isBusy }), Encoding.UTF8, "application/json");

                var responseTask = hc.PostAsync(stateUri, data);
                var response = responseTask.Result;

            }
            catch (AggregateException)
            {
                throw new CachngeTableStateException();
            }
        }
    }
}
