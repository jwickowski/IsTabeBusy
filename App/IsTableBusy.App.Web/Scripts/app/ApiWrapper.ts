/// <reference path="../typings/jquery/jquery.d.ts" />


class ApiWrapper {
    private baseUrl: string = "http://localhost:64598";

    getTables(placeName: string): JQueryXHR {
        var url: string = this.baseUrl + "/places/{placeName}/tables";
        url = url.replace('{placeName}', placeName);

        var promise: JQueryXHR = $.ajax({
            url: url,
            method: "GET",
            dataType: "JSONP"
        });
        return promise;
    }

    public setBusy(placeName: string, tableId: number, isBusy: boolean): JQueryXHR{
        var url: string = this.baseUrl + "/places/{placeName}/tables/{tableId}/{actionName}";
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