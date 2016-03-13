/// <reference path="../../typings/knockout/knockout.d.ts" />

class Place {
    id: number;
    name: string;

    constructor(item: any) {
        this.id = item.id;
        this.name = item.name;
    }
}
export = Place