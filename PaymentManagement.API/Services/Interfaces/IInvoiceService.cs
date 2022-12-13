using PaymentManagement.API.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentManagement.API.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<List<InvoicePayment>> GetAll();
        Task<InvoicePayment> GetById(string id);
        Task Add(InvoicePayment invoicePayment);
        Task Delete(string id);
    }
}
