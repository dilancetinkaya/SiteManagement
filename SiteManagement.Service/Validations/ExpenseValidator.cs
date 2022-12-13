using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class ExpenseValidator : AbstractValidator<CreateExpenseDto>
    {
        public ExpenseValidator()
        {
            RuleFor(x => x.Price).NotNull();
            RuleFor(x => x.ExpenseTypeId).NotNull();
            RuleFor(x => x.FlatId).NotNull();
        }
    }
}
