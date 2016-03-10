/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="Device.ts" />

import Device = require("Device");
import ApiWrapper = require("../ApiWrapper");

class SelectDeviceViewModel {
    public avaliableDevices: KnockoutObservableArray<Device>;
    public selectedDevice: Device;

    constructor() {
        
    }


}

export = SelectDeviceViewModel;