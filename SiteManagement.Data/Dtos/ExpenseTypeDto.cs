using SiteManagement.Domain.Entities;
using System.Collections.Generic;

namespace SiteManagement.Infrastructure.Dtos
{
    public class ExpenseTypeDto
    {
        public int Id { get; set; }
        public string ExpenseTypeName { get; set; }
    }
}
