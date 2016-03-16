namespace IsTableBusy.Core.Places
{
    public interface PlacesHubWrapper
    {
        void IsBusy(int tableId);
        void IsFree(int tableId);
    }
}