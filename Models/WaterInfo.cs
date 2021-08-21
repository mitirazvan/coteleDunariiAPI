using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoteleDunarii.Models
{
    public class WaterInfo
    {
        public int WaterInfoId { get; set; }
        public string Elevation { get; set; }
        public int Variation { get; set; }
        public string Temperature { get; set; }
        public DateTime ReadTime { get; set; }
    }
}
