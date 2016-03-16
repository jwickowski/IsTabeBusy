using IsTableBusy.Core.Places;
using Microsoft.AspNet.SignalR;
using System;

namespace IsTableBusy.App.Api.Hubs
{
    public class PlacesHubWrapperImp : PlacesHubWrapper
    {
        Lazy<IHubContext> hub = new Lazy<IHubContext>(
          () => GlobalHost.ConnectionManager.GetHubContext<PlacesHub>()
      );

        protected IHubContext Hub
        {
            get { return hub.Value; }
        }

        public void IsBusy(int tableId)
        {
            Hub.Clients.All.isBusy(tableId);
        }

        public void IsFree(int tableId)
        {
            Hub.Clients.All.isFree(tableId);
        }
    }
}