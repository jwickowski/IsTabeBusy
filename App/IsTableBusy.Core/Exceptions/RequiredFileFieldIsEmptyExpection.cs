using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
