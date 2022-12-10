using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SiteManagement.Domain.Entities
{
    public class User : IdentityUser, IEntity
    {
        public string IdentificationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CarLicensePlate { get; set; }
        public ICollection<Flat> Flats { get; set; }
    }
}
