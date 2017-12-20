using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using IsTableBusy.EntityFramework;

namespace IsTableBusy.Core.Models
{
    public class PlaceViewModelValidator : AbstractValidator<PlaceViewModel>
    {
        private readonly Context context;

        public PlaceViewModelValidator(Context context)
        {
            this.context = context;
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).NotNull();
            this.Custom(UniqueName);
        }

        private ValidationFailure UniqueName(PlaceViewModel place)
        {
            if (context.Places.Any(x => x.Name == place.Name))
            {
                return new ValidationFailure("Name", "Name must be unique");
            }
            return null;
        }
    }
}
