using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Domain.PaymentApiModel;
using SiteManagement.Infrastructure.IServices;
using SiteManagement.Infrastructure.IServices.APIServices;
using System.Threading.Tasks;

namespace SiteManagement.API.Controllers
{
    [Authorize(Roles = "Admin,User", AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public PaymentController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentDto createPaymentDto)
        {
           await _expenseService.AddPayment(createPaymentDto);
            return Ok();
        }
    }
}
