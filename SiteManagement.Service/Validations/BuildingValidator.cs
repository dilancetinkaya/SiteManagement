using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class BuildingValidator : AbstractValidator<CreateBuildingDto>
    {
        public BuildingValidator()
        {
            RuleFor(b => b.BuildingName).NotNull().WithMessage("{PropertyName} is required")
                                         .NotEmpty().WithMessage("{PropertyName} is required");


        }
    }
}
