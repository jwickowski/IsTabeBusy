namespace Tazos.Tools.XUnit
{
    public interface DatabaseCreator
    {
        void Create(string connectionString);
    }
}