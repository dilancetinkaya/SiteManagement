namespace SiteManagement.Infrastructure.Dtos
{
    public class UpdateFlatDto
    {
        public byte FlatNumber { get; set; }
        public bool IsEmpty { get; set; } = false;
        public bool IsOwner { get; set; } = false;
    }
}
