using System.Collections.Generic;

namespace SiteManagement.Domain.Entities
{
    public class Block : IEntity
    {
        public int Id { get; set; }
        public string BlockName { get; set; }
        public ICollection<Building> Buildings { get; set; }
    }
}
