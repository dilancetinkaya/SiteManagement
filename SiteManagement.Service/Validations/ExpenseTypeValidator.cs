using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class ExpenseTypeValidator : AbstractValidator<CreateExpenseTypeDto>
    {
        public ExpenseTypeValidator()
        {
            RuleFor(x => x.ExpenseTypeName).NotNull().WithMessage("Expense Type is required")
                                           .NotEmpty().WithMessage("Expense Type is required");
        }
    }
}
