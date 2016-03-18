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
        private readonly TableManager tableManager;
        private readonly TableTurningValidator tableTurningValidator;
        private readonly TableReader tableReader;
        private readonly AllPlacesReader allPlacesReader;
        private readonly PlaceInserter placeInserter;
        private readonly PlaceUpdater placeUpdater;
        private readonly PlaceRemover placeRemover;
        private PlacesHubWrapper hub;

        public PlacesController(
            TablesInPlaceReader tablesInPlaceReader, 
            TableManager tableManager, 
            TableTurningValidator tableTurningValidator,
            TableReader tableReader,
            AllPlacesReader allPlacesReader,
            PlaceInserter placeInserter,
            PlaceUpdater placeUpdater,
            PlaceRemover placeRemover,
            PlacesHubWrapper hub)
        {
            this.tablesInPlaceReader = tablesInPlaceReader;
            this.tableManager = tableManager;
            this.tableTurningValidator = tableTurningValidator;
            this.tableReader = tableReader;
            this.allPlacesReader = allPlacesReader;
            this.placeInserter = placeInserter;
            this.placeUpdater = placeUpdater;
            this.placeRemover = placeRemover;
            this.hub = hub;
        }

        [Route("places")]
        public IEnumerable<PlaceViewModel> GetPlaces()
        {
            return this.allPlacesReader.Read();
        }
        [HttpPost]
        [Route("place")]
        public PlaceViewModel PostPlace(PlaceViewModel place)
        {
            this.placeInserter.Insert(place);
            return place;
        }
        [HttpPut]
        [Route("place")]
        public PlaceViewModel PutPlace(PlaceViewModel place)
        {
            this.placeUpdater.Update(place);
            return place;
        }

        [HttpDelete]
        [Route("place/{id}")]
        public void DeletePlace (int id)
        {
            this.placeRemover.Remove(id);
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
            hub.IsBusy(tableId);
        }

        [HttpPost]
        [Route("places/{placeName}/tables/{tableId:int}/setFree")]
        public void SetFree(string placeName, int tableId)
        {
            this.tableTurningValidator.Validate(placeName, tableId);
            this.tableManager.SetFree(tableId);
            hub.IsFree(tableId);
        }
    }
}
