/// <reference path="../../typings/knockout/knockout.d.ts" />
/// <reference path="TablesViewModel.ts" />

class PlaceTablesPage {
    public tablesViewModel: TablesViewModel;

    constructor() {
        var placeName = window['placeName'];
        this.tablesViewModel = new TablesViewModel();
        this.tablesViewModel.placeName = placeName;
    }

    public run(): void {
        this.tablesViewModel.run();
        ko.applyBindings(this.tablesViewModel);
    }
}
   
new PlaceTablesPage().run();
