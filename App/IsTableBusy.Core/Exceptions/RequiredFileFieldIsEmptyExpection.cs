using System;

namespace IsTableBusy.Core.Exceptions
{
    public class RequiredFileFieldIsEmptyExpection : Exception
    {
        public string Field { get; set; }

        public RequiredFileFieldIsEmptyExpection(string field)
        {
            Field = field;
        }
    }
}
