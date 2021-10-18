using AutoMapper;
using CoteleDunarii.Repository.Interfaces;
using CoteleDunarii.Services.Dtos;
using CoteleDunarii.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoteleDunarii.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepostirory _cityRepostirory;
        private readonly IMapper _mapper;

        public CityService(ICityRepostirory cityRepostirory, IMapper mapper)
        {
            _cityRepostirory = cityRepostirory;
            _mapper = mapper;
        }

        public async Task<List<CityDto>> GetCities()
        {
            var cities = await _cityRepostirory.GetCitiesAsync();

            List<CityDto> dtos = _mapper.Map<List<CityDto>>(cities);

            return dtos;
        }

        public async Task<CityDto> GetCity(string name)
        {
            var city = await _cityRepostirory.GetCityAsync(name);
            return _mapper.Map<CityDto>(city);
        }

        public async Task SaveCity(CityDto city)
        {
            // check if city exists:
            var cityFromDb = await _cityRepostirory.GetCityAsync(city.Name);
            if (cityFromDb == null)
            {
                var entity = _mapper.Map<Data.Models.City>(city);
                await _cityRepostirory.SaveCityAsync(entity);
            }
            else
            {
                var waterEstEntity = _mapper.Map<Data.Models.WaterEstimations>(city.WaterEstimations[0]);
                await _cityRepostirory.AddWaterEstimationsAsync(cityFromDb.CityId, waterEstEntity);

                var waterInfoEntity = _mapper.Map<Data.Models.WaterInfo>(city.WaterInfos[0]);
                await _cityRepostirory.AddWaterInfoAsync(cityFromDb.CityId, waterInfoEntity);
            }
        }
    }
}
