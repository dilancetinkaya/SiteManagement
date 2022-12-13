using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class FlatValidator: AbstractValidator<CreateFlatDto>
    {
        public FlatValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.TypeOfFlat).NotEmpty().WithMessage("başarız");
          
            
        }
    }
}
