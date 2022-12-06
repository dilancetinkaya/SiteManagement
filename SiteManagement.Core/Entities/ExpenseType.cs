using System.Collections.Generic;

namespace SiteManagement.Domain.Entities
{
    public class ExpenseType : IEntity
    {
        public int Id { get; set; }
        public string ExpenseTypeName { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
