/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="PlacesViewModel.ts" />

import PlacesViewModel = require("PlacesViewModel");

class HomePage {
    public placesViewModel: PlacesViewModel;

    constructor() {
        this.placesViewModel = new PlacesViewModel();
    }

    public run(): void {
        this.placesViewModel.run();
        ko.applyBindings(this.placesViewModel);
    }
}

new HomePage().run();