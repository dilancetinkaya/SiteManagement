using System;

namespace SiteManagement.Infrastructure.Dtos
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public bool IsPaid { get; set; }
        public decimal Price { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string UserId { get; set; }
        public int FlatId { get; set; }
        public int ExpenseTypeId { get; set; }
    }
}
