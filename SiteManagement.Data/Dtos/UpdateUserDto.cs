using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.Dtos
{
    public class UpdateUserDto
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentificationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CarLicensePlate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
