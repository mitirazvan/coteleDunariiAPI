using CoteleDunarii.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoteleDunarii.Repository.Interfaces
{
    public interface ICityRepostirory
    {
        public Task<City> GetCityAsync(string name);

        public Task<int> GetCityIdAsync(string name);

        public Task<List<City>> GetCitiesAsync();

        public Task<bool> SaveCityAsync(City city);

        public Task<bool> AddWaterInfoAsync(int cityId, WaterInfo waterInfo);

        public Task<bool> AddWaterEstimationsAsync(int cityId, WaterEstimations waterEstimations);

    }
}
