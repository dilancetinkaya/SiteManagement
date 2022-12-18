namespace SiteManagement.Infrastructure.Dtos
{
    public class UpdateFlatUserDto
    {
        public bool IsOwner { get; set; } = false;
        public string UserId { get; set; }
    }
}
