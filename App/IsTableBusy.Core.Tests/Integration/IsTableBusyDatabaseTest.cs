using System;
using IsTableBusy.EntityFramework;
using Tazos.Tools.XUnit;

namespace IsTableBusy.Core.Tests.Integration
{
    public class IsTableBusyDatabaseTest: DatabaseTest, IDisposable
    {
        public IsTableBusyDatabaseTest() : base("DefaultConnection")
        {
        }

        protected override DatabaseCreator DatabaseCreator => new IsTabeBusyDatabaseTool();

        protected override DatabaseRemover DatabaseRemover => new IsTabeBusyDatabaseTool();
    }
}
