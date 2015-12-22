/// <reference path="../typings/jquery/jquery.d.ts" />


class ApiWrapper {
    getTables(placeName: string): JQueryXHR {
        var promise: JQueryXHR = $.ajax({
            url: "http://localhost:64598/places/place1/tables", method: "GET", dataType: "JSONP"
        });
        return promise;
    }

}

export = ApiWrapper