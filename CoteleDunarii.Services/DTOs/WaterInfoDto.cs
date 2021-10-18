using System;

namespace CoteleDunarii.Services.Dtos
{
    public class WaterInfoDto
    {
        public string Elevation { get; set; }
        public int Variation { get; set; }
        public string Temperature { get; set; }
        public DateTime ReadTime { get; set; }
    }
}
