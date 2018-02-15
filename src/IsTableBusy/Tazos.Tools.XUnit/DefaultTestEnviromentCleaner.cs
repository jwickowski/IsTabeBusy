namespace Tazos.Tools.XUnit
{
    public class DefaultTestEnviromentCleaner
    {
        private readonly DatabaseRemover databaseRemover;

        public DefaultTestEnviromentCleaner(DatabaseRemover databaseRemover)
        {
            this.databaseRemover = databaseRemover;
        }

        public void Clean(DefaultTestToken token)
        {
            databaseRemover.Remove(token.ConnectionString);
        }
    }
}
