namespace SiteManagement.Infrastructure.Dtos
{
    public class FlatDto
    {
        public int Id { get; set; }
        public byte FlatNumber { get; set; }
        public byte FloorNumber { get; set; }
        public string TypeOfFlat { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsOwner { get; set; }
        public string UserId { get; set; }
        public int BuildingId { get; set; }
    }
}

