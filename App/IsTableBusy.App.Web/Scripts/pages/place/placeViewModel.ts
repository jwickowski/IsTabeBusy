/// <reference path="../../typings/jquery/jquery.d.ts" />
/// <reference path="../../typings/knockout/knockout.d.ts" />

interface Table {
    id: number;
    name: string;
    isBusy: boolean;
}

interface Tables {
    [index: number]: Table;
}

class PlaceViewModel {
    public tables: any;
    public place: KnockoutObservableStatic;
    constructor() {
        this.tables = ko.observableArray();

        var promise: JQueryXHR = $.ajax({
            url: "http://localhost:64598/places/place1/tables", method: "GET", dataType:"JSONP"
        });

        promise.then((data) => {
            this.tables(data);
        });
    }
}

export = PlaceViewModel