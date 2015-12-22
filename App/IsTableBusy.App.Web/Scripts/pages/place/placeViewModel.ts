/// <reference path="../../typings/knockout/knockout.d.ts" />

interface Table {
    Id: number;
    Name: string;
    IsBusy: boolean;
}

interface Tables {
    [index: number]: Table;
}

class PlaceViewModel {
    public tables: Tables;
    constructor() {
        this.tables = [{ Id: 1, IsBusy: true, Name: 'Foo' }];
    }
}

export = PlaceViewModel