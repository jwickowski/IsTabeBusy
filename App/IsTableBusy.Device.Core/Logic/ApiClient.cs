using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using IsTableBusy.Device.Core.Exceptions;
using Newtonsoft.Json;


namespace IsTableBusy.Device.Core.Logic
{
    public sealed class ApiClient
    {
        private readonly Config config;

        public ApiClient(Config config)
        {
            this.config = config;
        }

        internal Table GetTable()
        {
            try
            {
                HttpClient hc = new HttpClient();
                Uri baseUri = new Uri(config.ApiUrl);
                Uri tablesUri = new Uri(baseUri, $"api/places/{config.PlaceName}/tables/{config.TableId}");
                var responseTask = hc.GetStringAsync(tablesUri);
                var response = responseTask.Result;
                return JsonConvert.DeserializeObject<Table>(response);
            }
            catch (AggregateException)
            {
                throw new ReadingTableException();
            }
        }

        internal void  SetBusy(bool isBusy)
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
