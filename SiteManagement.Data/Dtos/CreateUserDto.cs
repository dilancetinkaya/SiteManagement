namespace SiteManagement.Infrastructure.Dtos
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentificationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CarLicensePlate { get; set; }
        public string Email { get; set; }
    }
}
