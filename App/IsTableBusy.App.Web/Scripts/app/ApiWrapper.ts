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

