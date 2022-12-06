using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.Dtos
{
    public class CreateFlatDto
    {
        public byte FlatNumber { get; set; }
        public string TypeOfFlat { get; set; }
        public bool IsEmpty { get; set; }
        public string UserId { get; set; }
        public int BuildingId { get; set; }
    }
}
