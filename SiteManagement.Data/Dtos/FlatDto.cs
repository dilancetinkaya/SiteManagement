namespace SiteManagement.Infrastructure.Dtos
{
    public class FlatDto
    {
        public int Id { get; set; }
        public byte FlatNumber { get; set; }
        public string TypeOfFlat { get; set; }
        public bool IsEmpty { get; set; }
        public string UserId { get; set; }
        public string BuildingId{ get; set; }
    }
}
