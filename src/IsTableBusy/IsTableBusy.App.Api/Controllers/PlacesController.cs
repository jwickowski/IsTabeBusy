using IsTableBusy.Core;
using IsTableBusy.Core.Models;
using System.Collections.Generic;
using System.Web.Http;
using IsTableBusy.Core.Places;

namespace IsTableBusy.App.Api.Controllers
{

    public class PlacesController : ApiController
    {
        private readonly TablesInPlaceReader tablesInPlaceReader;
        private readonly TableReader tableReader;
        private readonly AllPlacesReader allPlacesReader;

        public PlacesController(
            TablesInPlaceReader tablesInPlaceReader, 
            TableReader tableReader,
            AllPlacesReader allPlacesReader)
        {
            this.tablesInPlaceReader = tablesInPlaceReader;
            this.tableReader = tableReader;
            this.allPlacesReader = allPlacesReader;
        }

        [Route("places")]
        public IEnumerable<PlaceViewModel> GetPlaces()
        {
            return this.allPlacesReader.Read();
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
            var result = this.tableReader.Read(placeName, tableId);
            return result;
        }
    }
}
