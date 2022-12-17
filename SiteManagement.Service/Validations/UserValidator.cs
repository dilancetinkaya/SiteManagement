using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class UserValidator : AbstractValidator<CreateUserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("{PropertyName} is required")
                                     .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.FirstName).MaximumLength(200).WithMessage("First Name ");
            RuleFor(x => x.LastName).NotNull().WithMessage("{PropertyName} is required")
                                     .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.LastName).MaximumLength(200);
            RuleFor(x => x.UserName).NotNull().WithMessage("{PropertyName} is required")
                                    .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.UserName).MaximumLength(200);
            RuleFor(x => x.Email).NotNull().WithMessage("{PropertyName} is required")
                                 .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.IdentificationNumber).NotNull().WithMessage("{PropertyName} is required")
                                                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.IdentificationNumber).MaximumLength(11);
            RuleFor(x => x.CarLicensePlate).NotNull().WithMessage("{PropertyName} is required")
                                           .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.CarLicensePlate).MaximumLength(20);
        }
    }
}
