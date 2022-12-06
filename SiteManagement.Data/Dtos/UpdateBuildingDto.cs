using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.Dtos
{
    public class UpdateBuildingDto
    {
        public string BuildingName { get; set; }
        public byte TotalFlat { get; set; }
        public int BlockId { get; set; }
    }
}
