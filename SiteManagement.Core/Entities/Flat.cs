using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteManagement.Domain.Entities
{
    public class Flat : IEntity
    {
        public int Id { get; set; }
        public byte FlatNumber { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsOwner { get; set; }
        public string TypeOfFlat { get; set; }//enum 
        public byte FloorNumber { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int BuildingId { get; set; }
        [ForeignKey("BuildingId")]
        public Building Building { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
    public enum TypeOfFlat
    {
        a=1+0,
        b = 1+1,
        c = 2+0,
        d = 2+1,
        e = 3+1,
        f = 3+2,
        g = 3+3,
        h = 4+0
    };

}

