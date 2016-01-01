namespace IsTableBusy.App.RaspberryPi.Logic
{
    public interface Light
    {
        void On();
        void Off();
        bool IsOn { get; }
    }
}
