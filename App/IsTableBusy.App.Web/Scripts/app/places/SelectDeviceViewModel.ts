/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="Device.ts" />

class SelectDeviceViewModel {
    public avaliableDevices: KnockoutObservableArray<Device>;
    public selectedDevice: Device;

    constructor() {
        
    }


}