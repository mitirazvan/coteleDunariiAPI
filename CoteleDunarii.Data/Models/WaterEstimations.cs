using System;

namespace CoteleDunarii.Data.Models
{
    public class WaterEstimations
    {
        public int WaterEstimationsId { get; set; }
        public string Next24h { get; set; }
        public string Next48h { get; set; }
        public string Next72h { get; set; }
        public string Next96h { get; set; }
        public string Next120h { get; set; }
        public DateTime ReadTime { get; set; }
        public City City { get; set; }
    }
}
