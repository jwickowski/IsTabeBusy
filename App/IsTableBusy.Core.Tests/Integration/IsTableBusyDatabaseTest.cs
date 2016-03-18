using System;
using IsTableBusy.EntityFramework;
using Tazos.Tools.XUnit;

namespace IsTableBusy.Core.Tests.Integration
{
    public class IsTableBusyDatabaseTest: DatabaseTest, IDisposable
    {
        protected Context context;
        public IsTableBusyDatabaseTest() : base("DefaultConnection")
        {
            context = new Context();
        }

        public void Dispose()
        {
            context.Dispose();
            base.Dispose();
        }

        protected override DatabaseCreator DatabaseCreator => new IsTabeBusyDatabaseTool();

        protected override DatabaseRemover DatabaseRemover => new IsTabeBusyDatabaseTool();
    }
}
