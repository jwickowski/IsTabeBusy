/// <reference path="../../typings/knockout/knockout.d.ts" />
class Table {
    id: number;
    name: string;
    isBusy: KnockoutObservable<boolean>;
    lastChangeStateDate: KnockoutObservable<Date>;

    constructor(item: any) {
        this.id = item.id;
        this.name = item.name;
        this.isBusy = ko.observable<boolean>(item.isBusy);
        this.lastChangeStateDate = ko.observable<Date>(item.lastChangeStateDate);
    }
}
