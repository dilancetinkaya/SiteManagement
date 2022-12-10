﻿using System;

namespace SiteManagement.Infrastructure.Dtos
{
    public class UpdateExpenseDto
    {
        public bool IsPaid { get; set; }
        public decimal Price { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int FlatId { get; set; }
        public string ExpenseTypeId { get; set; }
    }
}
