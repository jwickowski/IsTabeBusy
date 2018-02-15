using System;

namespace Tazos.Tools.XUnit
{
    public abstract class DatabaseTest : IDisposable
    {
        protected abstract DatabaseCreator DatabaseCreator { get; }
        protected abstract DatabaseRemover DatabaseRemover { get; }

        protected DatabaseTest(string connectionStringName)
        {
            TestEnviromentCleaner = new DefaultTestEnviromentCleaner(DatabaseRemover);
            TestEnviromentPreparer =
            new DefaultTestEnviromentPreparer(DatabaseCreator, connectionStringName);
            Token = TestEnviromentPreparer.Prepare();
        }

        public DefaultTestEnviromentCleaner TestEnviromentCleaner { get; set; }

        public DefaultTestEnviromentPreparer TestEnviromentPreparer { get; set; }

        protected readonly DefaultTestToken Token;

        public virtual void Dispose()
        {
            TestEnviromentCleaner.Clean(Token);
        }
    }
}
