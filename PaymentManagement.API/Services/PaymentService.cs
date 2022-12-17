using Microsoft.AspNetCore.Http;
using PaymentManagement.API.Models;
using PaymentManagement.API.Models.Dtos;
using PaymentManagement.API.Models.Entities;
using PaymentManagement.API.Services.Interfaces;
using PaymentManagement.API.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentManagement.API.Services
{
    public class PaymentService:IPaymentService
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ICreditCardInfoService _creditCardInfoService;

        public PaymentService(IInvoiceService invoiceService, ICreditCardInfoService creditCardInfoService)
        {
            _invoiceService = invoiceService;
            _creditCardInfoService = creditCardInfoService;
        }

        public async Task<ApiResponse<string>> CreatePayment(CreateInvoicePaymentDto createPaymentDto)
        {
            var validator = new CreateInvoicePaymentValidator();
            var results = validator.Validate(createPaymentDto);
            if (!results.IsValid)
            {
                return InternalServerError(results.ToString());
            }

            var creditCardResult = await _creditCardInfoService.GetByFilter(createPaymentDto);

            if (creditCardResult == null)
            {
                return InternalServerError("Geçersiz kredi kartı/ Kredi kartı bulunamadı.");
            }
            if (creditCardResult.Balance <= createPaymentDto.InvoiceAmount)
            {
                return InternalServerError("Yetersiz bakiye.");
            }
            var createInvoicePayment = new InvoicePayment()
            {
                CardNumber = createPaymentDto.CardNumber,
                Owner = createPaymentDto.Owner,
                Cvv = createPaymentDto.Cvv,
                InvoiceAmount = createPaymentDto.InvoiceAmount,
                ValidMonth = createPaymentDto.ValidMonth,
                ValidYear = createPaymentDto.ValidYear,
                FlatId = createPaymentDto.FlatId,
                ExpenseId = createPaymentDto.ExpenseId
            };
            creditCardResult.Balance -= createPaymentDto.InvoiceAmount;

            await _creditCardInfoService.Update(creditCardResult.Id, creditCardResult);
            await _invoiceService.Add(createInvoicePayment);
            return Success(createInvoicePayment.Id);
        }

        public async Task CreateCreditCard(CreditCardInfoDto creditCardInfoDto)
        {
            var createCreditCard = new CreditCardInfo()
            {
                CardNumber = creditCardInfoDto.CardNumber,
                Owner = creditCardInfoDto.Owner,
                Cvv = creditCardInfoDto.Cvv,
                Balance = creditCardInfoDto.Balance,
                ValidMonth = creditCardInfoDto.ValidMonth,
                ValidYear = creditCardInfoDto.ValidYear,
            };

            await _creditCardInfoService.Add(createCreditCard);
            
        }
        public async Task<ICollection<CreditCardInfo>> GetAllCreditİnfo()
        {
            var creditcartfan = await _creditCardInfoService.GetAllAsync();
            return creditcartfan;

        }


        private ApiResponse<string> Success(string data)
        {
            return new ApiResponse<string>
            {
                Data = data,
                Message = "İşlem başarılı",
                StatusCode = StatusCodes.Status200OK
            };
        }

        private ApiResponse<string> InternalServerError(string errorMessage)
        {
            return new ApiResponse<string>
            {
                Data = null,
                Message = errorMessage,
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
