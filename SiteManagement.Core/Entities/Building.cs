using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteManagement.Domain.Entities
{
    public class Building : IEntity
    {
        public int Id { get; set; }
        public string BuildingName { get; set; }
        public byte TotalFlat { get; set; }
        public int BlockId { get; set; }
        [ForeignKey("BlockId")]
        public Block Block { get; set; }
        public ICollection<Flat> Flats { get; set; }
    }
}
