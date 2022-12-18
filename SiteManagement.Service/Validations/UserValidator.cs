using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class UserValidator : AbstractValidator<CreateUserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("First Name is required")
                                     .NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.FirstName).MaximumLength(200).WithMessage("First Name must have less than 200 characters ");

            RuleFor(x => x.LastName).NotNull().WithMessage("Last Name is required")
                                     .NotEmpty().WithMessage("Last Name is required");
            RuleFor(x => x.LastName).MaximumLength(200).WithMessage("Last Name must have less than 200 characters ");

            RuleFor(x => x.UserName).NotNull().WithMessage("User Name is required")
                                    .NotEmpty().WithMessage("User Name is required");
            RuleFor(x => x.UserName).MaximumLength(200).WithMessage("User Name must have less than 200 characters "); ;

            RuleFor(x => x.Email).NotNull().WithMessage("Email is required")
                                 .NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid Email Address");

            RuleFor(x => x.IdentificationNumber).NotNull().WithMessage("Identification Number is required")
                                                .NotEmpty().WithMessage("Identification Number is required");
            RuleFor(x => x.IdentificationNumber).Must(x => x.Length > 11 && x.Length < 13)
                                                .WithMessage("Identification Number must have 12 characters");

            RuleFor(x => x.CarLicensePlate).NotNull().WithMessage("Car License Plate is required")
                                           .NotEmpty().WithMessage("Car License Plate is required");
            RuleFor(x => x.CarLicensePlate).MaximumLength(20).WithMessage("Car License Plate must have less than 20 characters ");
        }
    }
}
