using CoteleDunarii.Data;
using CoteleDunarii.Data.Models;
using CoteleDunarii.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<List<City>> GetCitiesAsync()
        {
            return await _context.Cities
                .Include(y => y.waterEstimations)
                .Include(z => z.waterInfos)
                .ToListAsync();
        }

        public async Task<City> GetCityAsync(string name)
        {
            return await _context.Cities.Where(x => x.Name == name)
                .Include(y => y.waterEstimations)
                .Include(z => z.waterInfos)
                .FirstOrDefaultAsync();
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
