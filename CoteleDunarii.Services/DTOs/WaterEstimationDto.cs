using System;

namespace CoteleDunarii.Services.DTOs
{
    public class WaterEstimationDto
    {
        public string Next24h { get; set; }
        public string Next48h { get; set; }
        public string Next72h { get; set; }
        public string Next96h { get; set; }
        public string Next120h { get; set; }
        public DateTime ReadTime { get; set; }
    }
}
