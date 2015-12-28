using System.Linq;
using IsTableBusy.Core.Exceptions;
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
            var tableExists = _context.Tables.Any(x => x.Place.Name == placeName && x.Id == tableId);
            if (tableExists == false)
            {
                throw new TableInWrongPlaceException();
            }
            
        }
    }
}