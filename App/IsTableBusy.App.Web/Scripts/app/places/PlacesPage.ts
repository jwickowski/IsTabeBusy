/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="../../vendor/jsgrid/jsGrid.d.ts" />
/// <reference path="placesdata.ts" />


import PlacesData = require('./PlacesData');
class PlacesPage {
    

    constructor(private selector: string) {
        
    }

    public run(): void {

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

            fields: [
                { name: "name", type: "text", width: 150 },
                { type: "control" }
            ]
        });
    }
}

new PlacesPage('#placesGrid').run();