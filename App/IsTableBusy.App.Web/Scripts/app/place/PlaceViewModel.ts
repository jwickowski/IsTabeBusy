/// <reference path="../../typings/jquery/jquery.d.ts" />
/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="../apiwrapper.ts" />

import ApiWrapper = require("./../ApiWrapper")

interface Table {
    id: number;
    name: string;
    isBusy: boolean;
}

interface Tables {
    [index: number]: Table;
}

class PlaceViewModel {
    private apiWrapper = new ApiWrapper();
    public tables: any;
    public place: string;
    constructor() {
        this.tables = ko.observableArray();

        var promise: JQueryXHR = this.apiWrapper.getTables(this.place);

        promise.then((data) => {
            this.tables(data);
        });
    }

    public SetBusy(item: Table) {
        alert("set busy for" + item.id);
    }

    public SetFree(item: Table) {
        alert("set free for" + item.id);
    }

}

export = PlaceViewModel