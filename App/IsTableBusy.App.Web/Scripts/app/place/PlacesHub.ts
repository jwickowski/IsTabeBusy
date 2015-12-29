/// <reference path="../config.ts" />
import config = require("./../config")

interface IsBusyChanged {
    (tableId: number): void;
}

class PlacesHub {
    private hub: HubProxy;
    constructor() {
       
    }

    public run() {
        var connection: HubConnection = $.hubConnection();
        connection.url = config.signalRUrl;
        connection.start().done(() => { console.log('done'); });
        this.hub = connection.createHubProxy('placesHub');
    }

    public isBusy = (func: IsBusyChanged) => {
        console.log("register busy");
        this.hub.on('isBusy', func);  
    };

    public isFree = (func: IsBusyChanged) => {
        console.log("register free");
        this.hub.on('isFree', func);
    };
}
export = PlacesHub