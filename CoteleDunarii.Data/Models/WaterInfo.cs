using System;

namespace CoteleDunarii.Data.Models
{
    public class WaterInfo
    {
        public int WaterInfoId { get; set; }
        public string Elevation { get; set; }
        public int Variation { get; set; }
        public string Temperature { get; set; }
        public DateTime ReadTime { get; set; }
        public City City { get; set; }
    }
}
