using FluentValidation;
using PaymentManagement.API.Models.Dtos;
using System;

namespace PaymentManagement.API.Validators
{
    public class CreditCardInfoValidator : AbstractValidator<CreditCardInfoDto>
    {
        public CreditCardInfoValidator()
        {
            RuleFor(x => x.Owner).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.CardNumber).NotEmpty().WithMessage("Card number is required").Length(16).WithMessage("Kredi Kartı Numarası 16 haneden az olamaz");
            RuleFor(x => x.ValidMonth).NotEmpty().WithMessage("Month is required").InclusiveBetween(1, 12).WithMessage("Değer 1-12 arası olmalıdır");
            RuleFor(x => x.ValidYear).NotEmpty().WithMessage("Year is required").InclusiveBetween(DateTime.Now.Year, 2100).WithMessage("Geçersiz yıl");
            RuleFor(x => x.Cvv).NotEmpty().WithMessage("CVV is required").Must(x => x >= 100 && x <= 999).WithMessage("Geçersiz CVV");
            RuleFor(x => x.Balance).NotEmpty().WithMessage("Balance is required").GreaterThan(0).WithMessage("Bakiye 0'dan az olamaz");


        }
    }
}
