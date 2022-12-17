using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class FlatValidator : AbstractValidator<CreateFlatDto>
    {
        public FlatValidator()
        {
            RuleFor(x => x.UserId).NotNull().WithMessage("User id is required")
                                         .NotEmpty().WithMessage("User id is required");
            RuleFor(x => x.TypeOfFlat).NotNull().WithMessage("TypeOfFlat is required")
                                         .NotEmpty().WithMessage("TypeOfFlat is required");
            RuleFor(x => x.FlatNumber).Must(x => x >= 1).WithMessage("Flat Number must be grater 0");
            RuleFor(x => x.FloorNumber).NotNull().WithMessage("TypeOfFlat is required")
                                         .NotEmpty().WithMessage("TypeOfFlat is required");
        }
    }
}
