namespace SiteManagement.Infrastructure.Dtos
{
    public class UpdateFlatDto
    {
        public byte FlatNumber { get; set; }
        public byte FloorNumber { get; set; }
        public string TypeOfFlat { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsOwner { get; set; }
        public int BuildingId { get; set; }
    }
}
