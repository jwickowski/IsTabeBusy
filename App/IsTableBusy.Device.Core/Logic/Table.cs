namespace IsTableBusy.App.RaspberryPi.Logic
{
    public sealed class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsBusy { get; set; }
    }
}