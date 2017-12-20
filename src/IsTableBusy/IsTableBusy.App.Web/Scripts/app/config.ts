 class Config {
     public static apiUrl(): string {
        return window['config'].apiUrl;
    };
    public static signalRUrl(): string {
        return window['config'].signalRUrl;
    }
}