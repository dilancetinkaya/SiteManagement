using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class MessageValidator : AbstractValidator<CreateMessageDto>
    {
        public MessageValidator()
        {
            RuleFor(x => x.MessageContent).NotNull().WithMessage("{PropertyName} is required")
                                          .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.SenderId).NotNull().WithMessage("{PropertyName} is required")
                                         .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.ReceiverId).NotNull().WithMessage("{PropertyName} is required")
                                         .NotEmpty().WithMessage("{PropertyName} is required");

        }
    }
}