using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsTableBusy.App.Api.Hubs
{
    public class PlacesHubWrapper
    {
        Lazy<IHubContext> hub = new Lazy<IHubContext>(
          () => GlobalHost.ConnectionManager.GetHubContext<PlacesHub>()
      );

        protected IHubContext Hub
        {
            get { return hub.Value; }
        }

        internal void IsBusy(int tableId)
        {
            Hub.Clients.All.isBusy(tableId);
        }

        internal void IsFree(int tableId)
        {
            Hub.Clients.All.isFree(tableId);
        }
    }
}