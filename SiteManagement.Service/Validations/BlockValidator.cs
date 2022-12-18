using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class BlockValidator : AbstractValidator<CreateBlockDto>
    {
        public BlockValidator()
        {
            RuleFor(x => x.BlockName).MinimumLength(2).WithMessage("Block Name must have more than 2 characters");
            RuleFor(x => x.BlockName).NotNull().WithMessage("Block Name is required");
            RuleFor(x => x.BlockName).NotEmpty().WithMessage("Block Name is required");
        }
    }
}
