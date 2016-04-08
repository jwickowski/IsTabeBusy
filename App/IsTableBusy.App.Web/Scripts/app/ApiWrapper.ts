/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="config.ts" />

class ApiWrapper {

    getPlaces(): JQueryXHR {
        var url: string = Config.apiUrl() + "/places";
        var promise: JQueryXHR = $.ajax({
            url: url,
            method: "GET",
            dataType: "JSONP"
        });
        return promise;
    } 

    postPlace(place: any): JQueryXHR {
        var url: string = Config.apiUrl() + "/place";
        var promise: JQueryXHR = $.ajax({
            url: url,
            method: "POST",
            data: place
        });
        return promise;
    }

    putPlace(place) {
        var url: string = Config.apiUrl() + "/place";
        var promise: JQueryXHR = $.ajax({
            url: url,
            method: "PUT",
            data: place
        });
        return promise;

    }

    deletePlace(place) {
        
        var url: string = Config.apiUrl() + "/place/" + place.id;
        var promise: JQueryXHR = $.ajax({
            url: url,
            method: "DELETE"
        });
        return promise;

    }

    getTables(placeName: string): JQueryXHR {
        var url: string = Config.apiUrl() + "/places/{placeName}/tables";
        url = url.replace('{placeName}', placeName);

        var promise: JQueryXHR = $.ajax({
            url: url,
            method: "GET",
            dataType: "JSONP"
        });
        return promise;
    } 
}

