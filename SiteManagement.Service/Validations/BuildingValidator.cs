using FluentValidation;
using SiteManagement.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Application.Validations
{
    public class BuildingValidator : AbstractValidator<CreateBuildingDto>
    {
        public BuildingValidator()
        {
            RuleFor(b => b.BuildingName).NotNull().WithMessage("{PropertyName} is required")
                                         .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(b => b.TotalFlat).NotNull().WithMessage("");

        }
    }
}
