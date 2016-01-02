using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Shapes;
using dotless.Core.Parser.Tree;
using IsTableBusy.App.RaspberryPi.Exceptions;
using Newtonsoft.Json;


namespace IsTableBusy.App.RaspberryPi.Logic
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
    }
}
