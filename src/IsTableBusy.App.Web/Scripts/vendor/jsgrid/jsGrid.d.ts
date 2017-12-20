interface JsGridDataSource {
    loadData(): JQueryPromise<{}>;
    insertItem(item: any): JQueryPromise<{}>    ;
    updateItem(item: any): JQueryPromise<{}>;
    deleteItem(item: any): JQueryPromise<{}>;
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

interface JsGridCallback
{
    (args: any): void;
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
    rowClick?: JsGridCallback;
}

interface JQuery {
    jsGrid(options: JsGridOptions): void;
}