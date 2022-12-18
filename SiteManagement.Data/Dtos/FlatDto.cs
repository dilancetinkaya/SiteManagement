using SiteManagement.Domain.Entities;

namespace SiteManagement.Infrastructure.Dtos
{
    public class FlatDto
    {
        public int Id { get; set; }
        public byte FlatNumber { get; set; }
        public byte FloorNumber { get; set; }
        public TypeOfFlat TypeOfFlat { get; set; }
        public bool IsEmpty { get; set; } = false;
        public bool IsOwner { get; set; } = false;
        public string UserId { get; set; }
        public int BuildingId { get; set; }
    }
}