namespace SiteManagement.Infrastructure.Dtos
{
    public class BuildingDto
    {
        public int Id { get; set; }
        public string BuildingName { get; set; }
        public byte TotalFlat { get; set; }
        public int BlockId { get; set; }
    }
}
