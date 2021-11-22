using CoteleDunarii.Data;
using CoteleDunarii.Data.Models;
using CoteleDunarii.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace CoteleDunarii.Repository
{
    public class CityRepository : ICityRepostirory
    {
        private CoteleDunariiContext _context;
        private ILogger<CityRepository> _log;

        public CityRepository(CoteleDunariiContext db, ILogger<CityRepository> logger)
        {
            _context = db;
            _log = logger;

        }

        public async Task<bool> AddWaterEstimationsAsync(int cityId, WaterEstimations waterEstimations)
        {
            var city = await _context.Cities.Where(x => x.CityId == cityId).FirstOrDefaultAsync();
            if (city != null && (await CanInsertWaterEstimationsAsync(cityId, waterEstimations.ReadTime)))
            {
                waterEstimations.City = city;
                try
                {
                    await _context.WaterEstimations.AddAsync(waterEstimations);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    _log.LogError(e, "Repository: failed to save WaterEstimation");
                }
            }
            return false;
        }

        public async Task<bool> AddWaterInfoAsync(int cityId, WaterInfo waterInfo)
        {
            var city = await _context.Cities.Where(x => x.CityId == cityId).FirstOrDefaultAsync();
            if (city != null && (await CanInsertWaterInfoAsync(cityId, waterInfo.ReadTime)))
            {
                waterInfo.City = city;
                try
                {
                    await _context.WaterInfos.AddAsync(waterInfo);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    _log.LogError(e, "Repository: failed to save waterInfo");
                }
            }
            return false;
        }

        public async Task<List<City>> GetCitiesAsync(DateTime? minDate = null)
        {
            if (minDate == null)
                minDate = DateTime.Today;

            var cities = await _context.Cities
                .IncludeFilter(y => y.waterEstimations.Where(y => y.ReadTime >= minDate))
                .IncludeFilter(z => z.waterInfos.Where(y => y.ReadTime >= minDate))
                .ToListAsync();

            foreach (var city in cities)
            {
                city.waterEstimations = city.waterEstimations.OrderBy(x => x.ReadTime).ToList();
                city.waterInfos = city.waterInfos.OrderBy(x => x.ReadTime).ToList();
            }

            return cities;
        }

        public async Task<City> GetCityAsync(string name, DateTime? minDate = null)
        {
            if (minDate == null)
                minDate = DateTime.Today;

            var result = await _context.Cities.Where(x => x.Name == name)
                .IncludeFilter(y => y.waterEstimations.Where(y => y.ReadTime >= minDate))
                .IncludeFilter(z => z.waterInfos.Where(y => y.ReadTime >= minDate))
                .FirstOrDefaultAsync();
            
            if(result != null) {
                result.waterEstimations = result.waterEstimations.OrderBy(x => x.ReadTime).ToList();
                result.waterInfos = result.waterInfos.OrderBy(x => x.ReadTime).ToList();
            }            

            return result;
        }

        public async Task<int> GetCityIdAsync(string name)
        {
            var city = await _context.Cities.Where(x => x.Name == name).FirstOrDefaultAsync();
            if (city != null)
                return city.CityId;
            else
            {
                _log.LogWarning($"Repository: City with {name} doesn't exists!");
                return -1;
            }

        }

        public async Task<bool> SaveCityAsync(City city)
        {
            try
            {
                await _context.AddAsync(city);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogError(e, "Repository: failed to save City");
                return false;
            }
        }

        private async Task<bool> CanInsertWaterInfoAsync(int cityId, DateTime readTime)
        {
            return !(await _context.WaterInfos.AnyAsync(x => x.City.CityId == cityId && x.ReadTime == readTime));
        }
        private async Task<bool> CanInsertWaterEstimationsAsync(int cityId, DateTime readTime)
        {
            return !(await _context.WaterEstimations.AnyAsync(x => x.City.CityId == cityId && x.ReadTime == readTime));
        }
    }
}
