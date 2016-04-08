/// <reference path="../../vendor/jsgrid/jsGrid.d.ts" />
/// <reference path="../apiwrapper.ts" />
class PlacesData implements JsGridDataSource {
    private apiWrapper: ApiWrapper;

    public constructor() {
        this.apiWrapper = new ApiWrapper();
    }

    loadData(): any {
        var promise = this.apiWrapper.getPlaces();
        return promise;
    }

    insertItem(item): any {
        var promise = this.apiWrapper.postPlace(item);
        return promise;
    }

    updateItem(item): any {
        var promise = this.apiWrapper.putPlace(item);
        return promise;
    }

    deleteItem(item): any {
        var promise = this.apiWrapper.deletePlace(item);
        return promise;
    }
}