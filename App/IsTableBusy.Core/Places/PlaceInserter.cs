using FluentValidation;
using IsTableBusy.Core.Models;
using IsTableBusy.EntityFramework;
using IsTableBusy.EntityFramework.Model;

namespace IsTableBusy.Core.Places
{
    public class PlaceInserter
    {
        private readonly Context context;
        private readonly PlaceViewModelValidator _validator;

        public PlaceInserter(Context context)
        {
            this.context = context;
            _validator = new PlaceViewModelValidator(context);
        }

        public void Insert(PlaceViewModel placeViewModel)
        {
            this._validator.ValidateAndThrow(placeViewModel);

            placeViewModel.Name = placeViewModel.Name.Trim();
            var itemToDb = new Place
            {
                Name = placeViewModel.Name
            };

            this.context.Places.Add(itemToDb);
            this.context.SaveChanges();

            placeViewModel.Id = itemToDb.Id;
        }
    }
}
