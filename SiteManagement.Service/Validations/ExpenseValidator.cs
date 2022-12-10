using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class ExpenseValidator : AbstractValidator<CreateExpenseDto>
    {
        public ExpenseValidator()
        {
            RuleFor(x => x.Price).NotNull().WithMessage("");
            RuleFor(x => x.ExpenseTypeId).NotNull().WithMessage("");
            RuleFor(x => x.FlatId).NotNull().WithMessage("");
        }
    }
}
