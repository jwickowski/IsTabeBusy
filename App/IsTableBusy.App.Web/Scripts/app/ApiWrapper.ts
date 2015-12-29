/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="config.ts" />
import config = require('./config');

class ApiWrapper {

    getTables(placeName: string): JQueryXHR {
        var url: string = config.apiUrl + "/places/{placeName}/tables";
        url = url.replace('{placeName}', placeName);

        var promise: JQueryXHR = $.ajax({
            url: url,
            method: "GET",
            dataType: "JSONP"
        });
        return promise;
    }

    public setBusy(placeName: string, tableId: number, isBusy: boolean): JQueryXHR{
        var url: string = config.apiUrl + "/places/{placeName}/tables/{tableId}/{actionName}";
        url = url.replace('{placeName}', placeName);
        url = url.replace('{tableId}', tableId.toString());

        if (isBusy) {
            url = url.replace('{actionName}', 'SetBusy');
        }
        else
        {
            url = url.replace('{actionName}', 'SetFree');
        }

        var promise: JQueryXHR = $.ajax({
            url: url,
            method: "POST"
        });
        return promise;
    }


}

export = ApiWrapper