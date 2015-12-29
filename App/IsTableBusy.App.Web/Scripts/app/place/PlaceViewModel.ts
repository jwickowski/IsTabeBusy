/// <reference path="../../typings/jquery/jquery.d.ts" />
/// <reference path="../../typings/lodash/lodash.d.ts" />
/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="../../typings/signalr/signalr.d.ts" />
/// <reference path="../apiwrapper.ts" />

/// <reference path="placeshub.ts" />

import ApiWrapper = require("../ApiWrapper")
import Table = require("Table");
import PlacesHub = require("PlacesHub");


class PlaceViewModel {
    private apiWrapper: ApiWrapper;
    public tables: KnockoutObservableArray<Table>;
    public placeName: string;
    private placesHub: PlacesHub;
    constructor() {
        this.tables = ko.observableArray<Table>();
        this.apiWrapper = new ApiWrapper();
        this.placesHub = new PlacesHub();
    }

    public run(): void {
        var promise: JQueryXHR = this.apiWrapper.getTables(this.placeName);

        promise.then((data) => {
            var mapped = _(data).map((item) => { return new Table(item); }).value();
            this.tables(mapped);
        });

        this.placesHub.run();

        this.placesHub.isBusy((tableId: number) => {
            var table: any = ko.utils.arrayFirst(this.tables(), (item) => {
                return item.id === tableId;
            });
            table.isBusy(true);
        });

        this.placesHub.isFree((tableId: number) => {
            var table: any = ko.utils.arrayFirst(this.tables(), (item) => {
                return item.id === tableId;
            });
            table.isBusy(false);
        });
    }

    public setBusy = (item: Table) => {
        this.apiWrapper.setBusy(this.placeName, item.id, true);
    }

    public setFree = (item: Table) => {
        this.apiWrapper.setBusy(this.placeName, item.id, false);
    }

}

export = PlaceViewModel