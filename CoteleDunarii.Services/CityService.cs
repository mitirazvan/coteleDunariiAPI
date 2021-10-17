using AutoMapper;
using CoteleDunarii.Repository.Interfaces;
using CoteleDunarii.Services.DTOs;
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

        public Task SaveCity(CityDto city)
        {
            throw new System.NotImplementedException();
        }
    }
}
