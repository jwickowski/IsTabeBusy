namespace Tazos.Tools.XUnit
{
    public class DefaultTestEnviromentPreparer
    {
        private readonly DatabaseCreator databaseCreator;
        private string connectionStringName;
        private static object _lock = new object();
        public DefaultTestEnviromentPreparer(DatabaseCreator databaseCreator, string connectionStringName)
        {
            this.databaseCreator = databaseCreator;
            this.connectionStringName = connectionStringName;
            this.ConnectionStringCreator = new DefaultConnectionStringCreator(connectionStringName);

        }

        private DefaultConnectionStringCreator ConnectionStringCreator { get; set; } 
        public DefaultTestToken Prepare()
        {
            var token = new DefaultTestToken();
            token.ConnectionString = ConnectionStringCreator.Create();

            CreateDatabase(token);

            return token;
        }

        public void CreateDatabase(DefaultTestToken token)
        {
            lock (_lock)
            {
                databaseCreator.Create(token.ConnectionString);
            }
           
        }
    }
}
