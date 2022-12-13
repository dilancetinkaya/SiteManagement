using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class BlockValidator : AbstractValidator<CreateBlockDto>
    {
        public BlockValidator()
        {
            RuleFor(x => x.BlockName).NotNull() .NotEmpty();
        }
    }
}
