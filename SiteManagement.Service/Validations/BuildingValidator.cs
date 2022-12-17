using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class BuildingValidator : AbstractValidator<CreateBuildingDto>
    {
        public BuildingValidator()
        {
            RuleFor(b => b.BuildingName).NotNull().WithMessage("Building is required")
                                         .NotEmpty().WithMessage("Building is required");
            RuleFor(b => b.BlockId).InclusiveBetween(1, int.MaxValue).WithMessage("Block id must be grater 0");
            RuleFor(b => b.TotalFlat).Must(x => x >= 1).WithMessage("Total Flat must be grater 0");

        }
    }
}
