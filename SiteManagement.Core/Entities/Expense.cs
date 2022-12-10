using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteManagement.Domain.Entities
{
    public class Expense : IEntity
    {
        public int Id { get; set; }
        public bool IsPaid { get; set; }
        public double Price { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int FlatId { get; set; }
        [ForeignKey("FlatId")]
        public Flat Flat { get; set; }
        public int ExpenseTypeId { get; set; }
        [ForeignKey("ExpenseTypeId")]
        public ExpenseType ExpenseType { get; set; }
    }
}
