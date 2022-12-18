using FluentValidation;
using SiteManagement.Infrastructure.Dtos;

namespace SiteManagement.Application.Validations
{
    public class MessageValidator : AbstractValidator<CreateMessageDto>
    {
        public MessageValidator()
        {
            RuleFor(x => x.MessageContent).NotNull().WithMessage("Message Context is required")
                                          .NotEmpty().WithMessage("Message Context is required");
            RuleFor(x => x.SenderId).NotNull().WithMessage("Sender Id is required")
                                         .NotEmpty().WithMessage("Sender Id is required");
         

        }
    }
}