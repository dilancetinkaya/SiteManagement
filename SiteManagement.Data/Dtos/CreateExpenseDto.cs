using System;

namespace SiteManagement.Infrastructure.Dtos
{
    public class CreateExpenseDto
    {
        public bool IsPaid { get; set; }
        public decimal Price { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int FlatId { get; set; }
        public int ExpenseTypeId { get; set; }
    }
}
