/// <reference path="../../typings/jquery/jquery.d.ts" />
/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="../apiwrapper.ts" />

import ApiWrapper = require("./../ApiWrapper")
import Table = require("Table");

interface Tables {
    [index: number]: Table;
}

class PlaceViewModel {
    private apiWrapper: ApiWrapper;
    public tables: any;
    public placeName: string;
    constructor() {
        this.tables = ko.observableArray();
        this.apiWrapper = new ApiWrapper();
       
    }

    public run(): void {
        var promise: JQueryXHR = this.apiWrapper.getTables(this.placeName);

        promise.then((data) => {
            this.tables(data);
        });
    }

    public setBusy =  (item: Table) => {
        this.apiWrapper.setBusy(this.placeName, item.id, true);
    }
        
    

    public setFree = (item: Table) => {
        this.apiWrapper.setBusy(this.placeName, item.id, false);
    }

}

export = PlaceViewModel