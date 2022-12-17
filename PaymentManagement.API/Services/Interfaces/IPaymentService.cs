using PaymentManagement.API.Models;
using PaymentManagement.API.Models.Dtos;
using PaymentManagement.API.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentManagement.API.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<ApiResponse<string>> CreatePayment(CreateInvoicePaymentDto createPaymentDto);
        Task CreateCreditCard(CreditCardInfoDto creditCardInfoDto);
        Task<ICollection<CreditCardInfo>> GetAllCreditİnfo();
    }
}
