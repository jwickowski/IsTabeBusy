using IsTableBusy.Core;
using IsTableBusy.Core.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using IsTableBusy.App.Api.Hubs;

namespace IsTableBusy.App.Api.Controllers
{

    public class PlacesController : ApiControllerWithHub<PlacesHub>
    {
        private readonly TableInPlaceReader tableInPlaceReader;
        private readonly TableManager tableManager;
        private readonly TableTurningValidator tableTurningValidator;

        public PlacesController(TableInPlaceReader tableInPlaceReader, TableManager tableManager, TableTurningValidator tableTurningValidator)
        {
            this.tableInPlaceReader = tableInPlaceReader;
            this.tableManager = tableManager;
            this.tableTurningValidator = tableTurningValidator;
        }


        [Route("places/{place}/tables")]
        public IEnumerable<TableViewModel> GetTablesForPlace(string place)
        {
            var result = this.tableInPlaceReader.Read(place);
            return result;
        }
        [HttpPost]
        [Route("places/{placeName}/tables/{tableId:int}/setBusy")]
        public void SetBusy(string placeName, int tableId)
        {
            this.tableTurningValidator.Validate(placeName, tableId);
            this.tableManager.SetBusy(tableId);
            Hub.Clients.All.isBusy(tableId);
        }
        [HttpPost]
        [Route("places/{placeName}/tables/{tableId:int}/setFree")]
        public void SetFree(string placeName, int tableId)
        {
            this.tableTurningValidator.Validate(placeName, tableId);
            this.tableManager.SetFree(tableId);
            Hub.Clients.All.isFree(tableId);
        }
    }
}
