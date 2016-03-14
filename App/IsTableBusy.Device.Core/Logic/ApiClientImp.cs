using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using IsTableBusy.Device.Core.Exceptions;
using Newtonsoft.Json;
using Windows.Storage;

namespace IsTableBusy.Device.Core.Logic
{
    public sealed class ApiClientImp :ApiClient
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
                Uri tablesUri = new Uri(baseUri, $"api/places/{config.PlaceName}/tables/{config.TableId}");
                var responseTask = hc.GetStringAsync(tablesUri);
                var response = responseTask.Result;
                var table =  JsonConvert.DeserializeObject<Table>(response);
                return table.IsBusy;
            }
            catch (AggregateException ex)
            {
                throw new Exception("Reading table error", ex);
            }
        }

        public void RegisterDevice()
        {
            //try
            //{
                HttpClient hc = new HttpClient();
                Uri baseUri = new Uri(config.ApiUrl);
                Uri registerUri = new Uri(baseUri, $"api/devices/register");
                var responseTask = hc.PostAsync(registerUri, null);
                var response = responseTask.Result;
            //ApplicationData.Current.lo
            //}
            //catch (AggregateException)
            //{
            //throw new Exception("Device registration error");
            //}
        }

        public void SetBusy(bool isBusy)
        {
            try
            {
                HttpClient hc = new HttpClient();
                Uri baseUri = new Uri(config.ApiUrl);
                Uri tablesUri;
                if (isBusy)
                {
                    tablesUri = new Uri(baseUri, $"api/places/{config.PlaceName}/tables/{config.TableId}/SetBusy");
                }
                else
                {
                    tablesUri = new Uri(baseUri, $"api/places/{config.PlaceName}/tables/{config.TableId}/SetFree");
                }
                
                var responseTask = hc.PostAsync(tablesUri, null);
                var response = responseTask.Result;
                
            }
            catch (AggregateException)
            {
                throw new CachngeTableStateException();
            }
        }
    }
}
