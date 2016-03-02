using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTableBusy.Core
{
    public static class DateTimeSupplier
    {
        private static DateTime _currentDataTime = DateTime.MinValue;

        public static DateTime Date
        {
            get
            {
                if (_currentDataTime == DateTime.MinValue)
                {
                    return DateTime.UtcNow;
                }
                return _currentDataTime;
            }
            set { _currentDataTime = value; }
        }
    }
}
