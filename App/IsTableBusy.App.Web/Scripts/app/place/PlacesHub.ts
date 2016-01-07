/// <reference path="../config.ts" />
import config = require("./../config")

interface IsBusyChanged {
    (tableId: number): void;
}

class PlacesHub {
    private hub: HubProxy;
    private isBusyFunctions = [];
    private isFreeFunctions = [];
    constructor() {
       
    }

    public run() {
        var connection: HubConnection = $.hubConnection();
        connection.url = config.signalRUrl;
        this.hub = connection.createHubProxy('placesHub');
        this.hub.on('isBusy', (tableId) => {
            for (var i = 0; i < this.isBusyFunctions.length; i++) {
                this.isBusyFunctions[i](tableId);
            }
        });  

        this.hub.on('isFree', (tableId) => {
            for (var i = 0; i < this.isFreeFunctions.length; i++) {
                this.isFreeFunctions[i](tableId);
            }
        });  

        connection.start();
        
    }

    public isBusy = (func: IsBusyChanged) => {
        this.isBusyFunctions.push(func);
    };

    public isFree = (func: IsBusyChanged) => {
        this.isFreeFunctions.push(func);
    };
}
export = PlacesHub