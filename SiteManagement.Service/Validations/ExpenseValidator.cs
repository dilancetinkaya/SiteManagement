using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class ExpenseValidator : AbstractValidator<CreateExpenseDto>
    {
        public ExpenseValidator()
        {
            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("Price  must be grater 0");
            RuleFor(x => x.ExpenseTypeId).InclusiveBetween(1, int.MaxValue).WithMessage("Expense id must be grater 0"); ;
            RuleFor(x => x.FlatId).InclusiveBetween(1, int.MaxValue).WithMessage("Flat id must be grater 0");
        }
    }
}
