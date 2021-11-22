using System.Collections.Generic;

namespace CoteleDunarii.Data.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Km { get; set; }

        public ICollection<WaterInfo> waterInfos { get; set; }

        public ICollection<WaterEstimations> waterEstimations { get; set; }
    }
}
