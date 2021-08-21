using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoteleDunarii.Models
{
    public class City
    {       
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Km { get; set; }
        public WaterInfo waterInfo { get; set; }
        public WaterEstimations waterEstimations { get; set; }
    }
}
