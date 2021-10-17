using CoteleDunarii.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoteleDunarii.Services.Interfaces
{
    public interface ICityService
    {
        Task<CityDto> GetCity(string name);

        Task<List<CityDto>> GetCities();

        Task SaveCity(CityDto city);
    }
}
