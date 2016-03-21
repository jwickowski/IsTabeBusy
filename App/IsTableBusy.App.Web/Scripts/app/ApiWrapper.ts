/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="config.ts" />

import config = require('./config');

class ApiWrapper {

    getPlaces(): JQueryXHR {
        var url: string = config.apiUrl() + "/places";
        var promise: JQueryXHR = $.ajax({
            url: url,
            method: "GET",
            dataType: "JSONP"
        });
        return promise;
    } 

    postPlace(place: any): JQueryXHR {
        var url: string = config.apiUrl() + "/place";
        var promise: JQueryXHR = $.ajax({
            url: url,
            method: "POST",
            data: place
        });
        return promise;
    }

    putPlace(place) {
        var url: string = config.apiUrl() + "/place";
        var promise: JQueryXHR = $.ajax({
            url: url,
            method: "PUT",
            data: place
        });
        return promise;

    }

    deletePlace(place) {
        
        var url: string = config.apiUrl() + "/place/" + place.id;
        var promise: JQueryXHR = $.ajax({
            url: url,
            method: "DELETE"
        });
        return promise;

    }

    getTables(placeName: string): JQueryXHR {
        var url: string = config.apiUrl() + "/places/{placeName}/tables";
        url = url.replace('{placeName}', placeName);

        var promise: JQueryXHR = $.ajax({
            url: url,
            method: "GET",
            dataType: "JSONP"
        });
        return promise;
    } 
}

export = ApiWrapper