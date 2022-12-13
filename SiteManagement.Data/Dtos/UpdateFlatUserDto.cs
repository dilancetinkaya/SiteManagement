using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.Dtos
{
    public class UpdateFlatUserDto
    {
        public bool IsOwner { get; set; }
        public string UserId { get; set; }
    }
}
