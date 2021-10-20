using CoteleDunarii.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoteleDunarii.Services.Interfaces
{
    public interface ICityService
    {
        Task<CityDto> GetCity(string name, DateTime? minDate = null);

        Task<List<CityDto>> GetCities(DateTime? minDate = null);

        Task SaveCity(CityDto city);
    }
}
