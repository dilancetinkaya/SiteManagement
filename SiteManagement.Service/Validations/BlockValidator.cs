using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class BlockValidator : AbstractValidator<CreateBlockDto>
    {
        public BlockValidator()
        {
            RuleFor(x => x.BlockName).MinimumLength(2).WithMessage("Blockname iki karakterdden az olamaz");
            RuleFor(x => x.BlockName).NotNull().WithMessage("null geçielmez");
            RuleFor(x => x.BlockName).NotEmpty().WithMessage("boş geçielmez");
        }
    }
}
