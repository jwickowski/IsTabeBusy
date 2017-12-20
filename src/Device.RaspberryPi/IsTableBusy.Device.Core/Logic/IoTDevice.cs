namespace IsTableBusy.Device.Core.Logic
{
    public interface IoTDevice
    {
        Light RedLight { get; }
        Light GreenLight { get; }
        Button Button { get; }
    }
}
