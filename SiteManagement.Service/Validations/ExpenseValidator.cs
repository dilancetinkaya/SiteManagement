using FluentValidation;
using SiteManagement.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
