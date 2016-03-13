/// <reference path="../../typings/jquery/jquery.d.ts" />
/// <reference path="../../typings/lodash/lodash.d.ts" />
/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="../apiwrapper.ts" />

import ApiWrapper = require("../ApiWrapper")
import Place = require("Place");

class PlacesViewModel {
    private apiWrapper: ApiWrapper;
    public places: KnockoutObservableArray<Place>;

    constructor() {
        this.apiWrapper = new ApiWrapper();
        this.places = ko.observableArray<Place>();
    }

    public run(): void {
        var promise: JQueryXHR = this.apiWrapper.getPlaces();

        promise.then((data) => {
            var mapped = _(data).map((item) => { return new Place(item); }).value();
            this.places(mapped);
        });
    }
}

export  = PlacesViewModel