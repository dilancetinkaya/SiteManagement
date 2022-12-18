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
            RuleFor(x => x.Email).EmailAddress().WithMessage("gecersiz email");

            RuleFor(x => x.IdentificationNumber).NotNull().WithMessage("{PropertyName} is required")
                                                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.IdentificationNumber).Must(x => x.Length > 11 && x.Length < 13)
                                                .WithMessage("12 haneli olmalı");

            RuleFor(x => x.CarLicensePlate).NotNull().WithMessage("{PropertyName} is required")
                                           .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.CarLicensePlate).MaximumLength(20).WithMessage("20 den büyük olamaz");
        }
    }
}
