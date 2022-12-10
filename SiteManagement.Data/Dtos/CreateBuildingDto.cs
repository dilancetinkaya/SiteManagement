namespace SiteManagement.Infrastructure.Dtos
{
    public class CreateBuildingDto
    {
        public string BuildingName { get; set; }
        public byte TotalFlat { get; set; }
        public int BlockId { get; set; }
    }
}
