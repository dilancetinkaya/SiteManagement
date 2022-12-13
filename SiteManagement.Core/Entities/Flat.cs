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
        public TypeOfFlat TypeOfFlat { get; set; }
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
        OnePlusOne,
        TwoPlusOne,
        TwoPlusTwo,
        ThreePlusOne,
        ThreePlusTwo,
        ThreePlusThree,
        FourPlusOne,
    };

}

