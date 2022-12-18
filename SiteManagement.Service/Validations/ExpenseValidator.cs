using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class ExpenseValidator : AbstractValidator<CreateExpenseDto>
    {
        public ExpenseValidator()
        {
            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("Price must be greater than 0");
            RuleFor(x => x.ExpenseTypeId).InclusiveBetween(1, int.MaxValue).WithMessage("Expense Type Id must be greater than 0"); ;
            RuleFor(x => x.FlatId).InclusiveBetween(1, int.MaxValue).WithMessage("Flat Id must be greater than 0");
        }
    }
}
