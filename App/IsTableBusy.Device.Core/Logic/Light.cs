namespace IsTableBusy.Device.Core.Logic
{
    public interface Light
    {
        void On();
        void Off();
        bool IsOn { get; }
    }
}
