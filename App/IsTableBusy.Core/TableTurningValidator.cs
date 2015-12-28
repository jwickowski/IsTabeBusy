using IsTableBusy.EntityFramework;

namespace IsTableBusy.Core
{
    public class TableTurningValidator
    {
        private readonly Context _context;

        public TableTurningValidator(Context context)
        {
            _context = context;
        }

        public void Validate(string placeName, int tableId)
        {
            
        }
    }
}