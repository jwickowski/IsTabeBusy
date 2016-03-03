using IsTableBusy.Core;
using IsTableBusy.Core.Models;
using System.Collections.Generic;
using System.Web.Http;
using IsTableBusy.App.Api.Hubs;

namespace IsTableBusy.App.Api.Controllers
{

    public class PlacesController : ApiControllerWithHub<PlacesHub>
    {
        private readonly TablesInPlaceReader tablesInPlaceReader;
        private readonly TableManager tableManager;
        private readonly TableTurningValidator tableTurningValidator;
        private readonly TableReader tableReader;
        private readonly AllPlacesReader allPlacesReader;

        public PlacesController(
            TablesInPlaceReader tablesInPlaceReader, 
            TableManager tableManager, 
            TableTurningValidator tableTurningValidator,
            TableReader tableReader,
            AllPlacesReader allPlacesReader)
        {
            this.tablesInPlaceReader = tablesInPlaceReader;
            this.tableManager = tableManager;
            this.tableTurningValidator = tableTurningValidator;
            this.tableReader = tableReader;
            allPlacesReader = allPlacesReader;
        }


        public IEnumerable<PlaceViewModel> Get()
        {
            return allPlacesReader.Read();
        }

        [Route("places/{place}/tables")]
        public IEnumerable<TableViewModel> GetTablesForPlace(string place)
        {
            var result = this.tablesInPlaceReader.Read(place);
            return result;
        }

        [Route("places/{placeName}/tables/{tableId:int}")]
        public TableViewModel GetTable(string placeName, int tableId)
        {
            this.tableTurningValidator.Validate(placeName, tableId);
            var result = this.tableReader.Read(placeName, tableId);
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
