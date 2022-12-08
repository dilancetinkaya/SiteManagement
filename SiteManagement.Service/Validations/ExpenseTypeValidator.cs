using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class ExpenseTypeValidator : AbstractValidator<CreateExpenseTypeDto>
    {
        public ExpenseTypeValidator()
        {
            RuleFor(x => x.ExpenseTypeName).NotEmpty();
        }
    }
}
