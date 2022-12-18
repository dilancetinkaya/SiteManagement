using Microsoft.AspNetCore.Mvc;
using PaymentManagement.API.Models;
using PaymentManagement.API.Models.Dtos;
using PaymentManagement.API.Services.Interfaces;
using System.Threading.Tasks;

namespace PaymentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("CreatePayment")]
        public async Task<ApiResponse<string>> CreatePayment(CreateInvoicePaymentDto createPaymentDto)
        {
            var payment = await _paymentService.CreatePayment(createPaymentDto);
            return payment;
        }

        [HttpPost("CreateCreditCard")]
        public async Task<IActionResult> CreateCreditCard(CreditCardInfoDto creditCardInfoDto)
        {
            await _paymentService.CreateCreditCard(creditCardInfoDto);
            return Ok();
        }

        [HttpGet("GetCreditCard")]
        public async Task<IActionResult> GetAll()
        {
            var creditcartfan = await _paymentService.GetAllCreditİnfo();
            return Ok(creditcartfan);

        }
      
    }
}
