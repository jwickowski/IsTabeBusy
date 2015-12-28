/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="placeviewmodel.ts" />

import PlaceViewModel = require("./PlaceViewModel");

class PlacePage {
    public placeViewModel: PlaceViewModel;

    constructor() {
        var placeName = window['placeName'];
        this.placeViewModel = new PlaceViewModel();
        this.placeViewModel.placeName = placeName;
    }

    public run(): void {
        this.placeViewModel.run();
        ko.applyBindings(this.placeViewModel);
    }
}
   
new PlacePage().run();