using System.Collections.Generic;

namespace CoteleDunarii.Services.Dtos
{
    public class CityDto
    {

        public string Name { get; set; }
        public string Km { get; set; }

        public List<WaterEstimationDto> WaterEstimations { get; set; }

        public List<WaterInfoDto> WaterInfos { get; set; }

    }
}
