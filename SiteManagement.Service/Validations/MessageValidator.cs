using FluentValidation;
using SiteManagement.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Application.Validations
{
    public class MessageValidator: AbstractValidator<CreateMessageDto>
    {
        public MessageValidator()
        {

        }
    }
}
