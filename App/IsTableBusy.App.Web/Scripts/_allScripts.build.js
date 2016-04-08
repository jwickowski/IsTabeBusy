var Config = (function () {
    function Config() {
    }
    Config.apiUrl = function () {
        return window['config'].apiUrl;
    };
    ;
    Config.signalRUrl = function () {
        return window['config'].signalRUrl;
    };
    return Config;
}());
/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="config.ts" />
var ApiWrapper = (function () {
    function ApiWrapper() {
    }
    ApiWrapper.prototype.getPlaces = function () {
        var url = Config.apiUrl() + "/places";
        var promise = $.ajax({
            url: url,
            method: "GET",
            dataType: "JSONP"
        });
        return promise;
    };
    ApiWrapper.prototype.postPlace = function (place) {
        var url = Config.apiUrl() + "/place";
        var promise = $.ajax({
            url: url,
            method: "POST",
            data: place
        });
        return promise;
    };
    ApiWrapper.prototype.putPlace = function (place) {
        var url = Config.apiUrl() + "/place";
        var promise = $.ajax({
            url: url,
            method: "PUT",
            data: place
        });
        return promise;
    };
    ApiWrapper.prototype.deletePlace = function (place) {
        var url = Config.apiUrl() + "/place/" + place.id;
        var promise = $.ajax({
            url: url,
            method: "DELETE"
        });
        return promise;
    };
    ApiWrapper.prototype.getTables = function (placeName) {
        var url = Config.apiUrl() + "/places/{placeName}/tables";
        url = url.replace('{placeName}', placeName);
        var promise = $.ajax({
            url: url,
            method: "GET",
            dataType: "JSONP"
        });
        return promise;
    };
    return ApiWrapper;
}());
/// <reference path="../../typings/jquery/jquery.d.ts" />
/// <reference path="../../typings/lodash/lodash.d.ts" />
/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="../apiwrapper.ts" />
var PlacesViewModel = (function () {
    function PlacesViewModel() {
        this.apiWrapper = new ApiWrapper();
        this.places = ko.observableArray();
    }
    PlacesViewModel.prototype.run = function () {
        var _this = this;
        var promise = this.apiWrapper.getPlaces();
        promise.then(function (data) {
            var mapped = _(data).map(function (item) { return new Place(item); }).value();
            _this.places(mapped);
        });
    };
    return PlacesViewModel;
}());
/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="PlacesViewModel.ts" />
var HomePage = (function () {
    function HomePage() {
        this.placesViewModel = new PlacesViewModel();
    }
    HomePage.prototype.run = function () {
        this.placesViewModel.run();
        ko.applyBindings(this.placesViewModel);
    };
    return HomePage;
}());
/// <reference path="../../typings/knockout/knockout.d.ts" />
var Place = (function () {
    function Place(item) {
        this.id = item.id;
        this.name = item.name;
    }
    return Place;
}());
/// <reference path="../config.ts" />
var PlacesHub = (function () {
    function PlacesHub() {
        var _this = this;
        this.isBusyFunctions = [];
        this.isFreeFunctions = [];
        this.isBusy = function (func) {
            _this.isBusyFunctions.push(func);
        };
        this.isFree = function (func) {
            _this.isFreeFunctions.push(func);
        };
    }
    PlacesHub.prototype.run = function () {
        var _this = this;
        var connection = $.hubConnection();
        connection.url = Config.signalRUrl();
        this.hub = connection.createHubProxy('placesHub');
        this.hub.on('isBusy', function (tableId) {
            for (var i = 0; i < _this.isBusyFunctions.length; i++) {
                _this.isBusyFunctions[i](tableId);
            }
        });
        this.hub.on('isFree', function (tableId) {
            for (var i = 0; i < _this.isFreeFunctions.length; i++) {
                _this.isFreeFunctions[i](tableId);
            }
        });
        connection.start();
    };
    return PlacesHub;
}());
/// <reference path="../../typings/jquery/jquery.d.ts" />
/// <reference path="../../typings/lodash/lodash.d.ts" />
/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="../../typings/signalr/signalr.d.ts" />
/// <reference path="../apiwrapper.ts" />
/// <reference path="placeshub.ts" />
var TablesViewModel = (function () {
    function TablesViewModel() {
        this.tables = ko.observableArray();
        this.apiWrapper = new ApiWrapper();
        this.placesHub = new PlacesHub();
    }
    TablesViewModel.prototype.run = function () {
        var _this = this;
        var promise = this.apiWrapper.getTables(this.placeName);
        promise.then(function (data) {
            var mapped = _(data).map(function (item) { return new Table(item); }).value();
            _this.tables(mapped);
        });
        this.placesHub.run();
        this.placesHub.isBusy(function (tableId) {
            var table = ko.utils.arrayFirst(_this.tables(), function (item) {
                return item.id === tableId;
            });
            table.isBusy(true);
        });
        this.placesHub.isFree(function (tableId) {
            var table = ko.utils.arrayFirst(_this.tables(), function (item) {
                return item.id === tableId;
            });
            table.isBusy(false);
        });
    };
    return TablesViewModel;
}());
/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="TablesViewModel.ts" />
var PlaceTablesPage = (function () {
    function PlaceTablesPage() {
        var placeName = window['placeName'];
        this.tablesViewModel = new TablesViewModel();
        this.tablesViewModel.placeName = placeName;
    }
    PlaceTablesPage.prototype.run = function () {
        this.tablesViewModel.run();
        ko.applyBindings(this.tablesViewModel);
    };
    return PlaceTablesPage;
}());
/// <reference path="../../typings/knockout/knockout.d.ts" />
var Table = (function () {
    function Table(item) {
        this.id = item.id;
        this.name = item.name;
        this.isBusy = ko.observable(item.isBusy);
    }
    return Table;
}());
//# sourceMappingURL=_allScripts.build.js.map