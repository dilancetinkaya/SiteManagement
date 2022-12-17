using SiteManagement.Domain.PaymentApiModel;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.IServices.APIServices
{
    public interface IPaymentAPIService
    {
        Task<ApiResponse<string>> CreatePayment(CreatePaymentDto createPaymentDto);
    }
}
