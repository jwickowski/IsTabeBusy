interface JsGridDataSource {
    loadData(): JQueryPromise<{}>;
    insertItem(item: any): void;
    updateItem(item: any): void;
    deleteItem(item: any): void;
}


interface JsGridField {
    name?: string;
    width?: number;
    type?: string;
    items?: JsGridDataSource;
    valueField?: string;
    textField?: string;
    title?: string;
    sorting?: boolean;

}

interface JsGridDataSource {
    
}

interface JsGridOptions {
    width: string;
    height: string;
    inserting: boolean;
    filtering: boolean;
    autoload?: boolean;
    editing: boolean;
    sorting: boolean;
    paging: boolean;
    controller?: JsGridDataSource;
    fields?: JsGridField[];
}

interface JQuery {
    jsGrid(options: JsGridOptions): void;
}