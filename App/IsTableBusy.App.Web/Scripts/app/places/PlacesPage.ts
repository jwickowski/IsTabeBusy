/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="../../vendor/jsgrid/jsGrid.d.ts" />
/// <reference path="placesdata.ts" />
/// <reference path="selectdeviceviewmodel.ts" />
/// <reference path="../../typings/bootstrap/bootstrap.d.ts" />

import PlacesData = require('./PlacesData');
import SelectDeviceViewModel = require("SelectDeviceViewModel");

class PlacesPage {
    private selectDeviceViewModel: SelectDeviceViewModel;

    constructor(private selector: string, private selectDeviceSelector: string) {
        this.selectDeviceViewModel = new SelectDeviceViewModel();
    }

    public run(): void {
        this.initGrid();
        debugger;
        ko.applyBindings(this.selectDeviceViewModel, $(this.selectDeviceSelector)[0]);
    }

    private initGrid() {
        var data = new PlacesData();
        $(this.selector).jsGrid({
            width: "100%",
            height: "400px",
            inserting: true,
            filtering: false,
            editing: true,
            sorting: true,
            paging: true,
            autoload: true,
            controller: data,
            rowClick: this.showSelectDevice,
            fields: [
                { name: "name", type: "text", width: 150 },
                { type: "control" }
            ]
        });
    }

    private showSelectDevice = (args: any): void => {
        $(this.selectDeviceSelector).modal({});
    }

}

$(() => {
    new PlacesPage('#placesGrid', '#selectDeviceModal').run();
});