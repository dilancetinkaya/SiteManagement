using PaymentManagement.API.Models.Dtos;
using PaymentManagement.API.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentManagement.API.Services.Interfaces
{
    public interface ICreditCardInfoService
    {
        Task<ICollection<CreditCardInfo>> GetAllAsync();
        Task<CreditCardInfo> GetById(string id);
        Task<CreditCardInfo> GetByFilter(CreateInvoicePaymentDto filter);
        Task Add(CreditCardInfo creditCardInfo);
        Task Delete(string id);
        Task Update(string id, CreditCardInfo creditCardInfo);
    }
}
