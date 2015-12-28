/// <reference path="../../typings/jquery/jquery.d.ts" />
/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="../../typings/signalr/signalr.d.ts" />
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
    private hub: any;
    constructor() {
        this.tables = ko.observableArray();
        this.apiWrapper = new ApiWrapper();
   
       
    }

    public run(): void {
        var promise: JQueryXHR = this.apiWrapper.getTables(this.placeName);

        promise.then((data) => {
            this.tables(data);
        });


        $.connection.hub.url = "http://localhost:64598/signalr";
        var connection = $.hubConnection();
        connection.url = "http://localhost:64598/signalr";
        var contosoChatHubProxy = connection.createHubProxy('placesHub');
        contosoChatHubProxy.on('isBusy', function (x) {
            console.log('isBusy');
        });
        contosoChatHubProxy.on('isFree', function (x) {
            console.log('isBusy');
        });
        connection.start();
    }

    public setBusy = (item: Table) => {
        this.apiWrapper.setBusy(this.placeName, item.id, true);
    }

    public setFree = (item: Table) => {
        this.apiWrapper.setBusy(this.placeName, item.id, false);
    }

}

export = PlaceViewModel