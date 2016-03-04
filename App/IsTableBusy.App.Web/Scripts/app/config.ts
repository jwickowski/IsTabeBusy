class Config {
    public apiUrl(): string {
        return window['config'].apiUrl;
    };
    public signalRUrl(): string {
        return window['config'].signalRUrl;
    }
}

export = new Config();

