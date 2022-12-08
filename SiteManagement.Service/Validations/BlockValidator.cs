using FluentValidation;
using SiteManagement.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Application.Validations
{
    public class BlockValidator:AbstractValidator<CreateBlockDto>
    {
        public BlockValidator()
        {
            RuleFor(x => x.BlockName).NotNull().WithMessage("Block Name can't be null");
        }
    }
}
