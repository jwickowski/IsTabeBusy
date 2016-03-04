/// <reference path="../../vendor/jsgrid/jsGrid.d.ts" />

class PlacesData implements JsGridDataSource {
    private places = [
        { Id: 1, Name: "place1" },
        { Id: 2, Name: "place2" }
    ];

    loadData(): any {
        var deferred = $.Deferred();
        var promis = deferred.promise();
        setTimeout(() => {
            deferred.resolve(this.places);
        }, 2000);

        return promis;
    }

    insertItem(item): void {
        this.places.push(item);
    }

    updateItem(item): void { throw new Error("Not implemented"); }

    deleteItem(item): void { throw new Error("Not implemented"); }
}

export = PlacesData;