using System;

namespace Tazos.Tools.XUnit
{
    public class DefaultConnectionStringCreator
    {
        private readonly string connectionStringName;

        public DefaultConnectionStringCreator(string connectionStringName)
        {
            this.connectionStringName = connectionStringName;
        }

        public string Create()
        {
            var randomValue = Guid.NewGuid();
            var databaseName = $"TestDatabase_{randomValue}";

            var cs = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            cs = cs.Replace("{Database}", databaseName);

            return cs;
        }
    }
}
