/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="placeviewmodel.ts" />

import PlaceViewModel = require("./PlaceViewModel");

class PlacePage {
    public placeViewModel: PlaceViewModel;

    constructor() {
        this.placeViewModel = new PlaceViewModel();
    }

    public run(): void {
        ko.applyBindings(this.placeViewModel);
    }
}
   
new PlacePage().run();