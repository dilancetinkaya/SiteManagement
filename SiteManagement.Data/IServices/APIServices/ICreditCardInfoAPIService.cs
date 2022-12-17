using SiteManagement.Domain.PaymentApiModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.IServices.APIServices
{
    public interface ICreditCardInfoAPIService
    {
        Task<ICollection<GetCreditCardDto>> GetAll();
    }
}

