using IsTableBusy.Core;
using IsTableBusy.Core.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace IsTableBusy.App.Api.Controllers
{

    public class PlacesController : ApiController
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
        [Route("places/{placeName}/tables/{tableId:int}/busy")]
        public void SetBusy(string placeName, int tableId)
        {
            this.tableTurningValidator.Validate(placeName, tableId);
            this.tableManager.SetBusy(tableId);
        }

        [HttpPost]
        [Route("places/{placeName}/tables/{tableId:int}/busy")]
        public void SetFree(string placeName, int tableId)
        {
            this.tableTurningValidator.Validate(placeName, tableId);
            this.tableManager.SetFree(tableId);
        }
    }
}
