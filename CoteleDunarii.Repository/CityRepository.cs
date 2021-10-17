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
        private CoteleDunariiContext Context;
        private ILogger<CityRepository> Log;

        public CityRepository(CoteleDunariiContext db, ILogger<CityRepository> logger)
        {
            Context = db;
            Log = logger;

        }

        public async Task<bool> AddWaterEstimationsAsync(int CityId, WaterEstimations waterEstimations)
        {
            var city = await Context.Cities.Where(x => x.CityId == CityId).FirstOrDefaultAsync();
            if (city != null)
            {
                waterEstimations.City = city;
                try
                {
                    await Context.WaterEstimations.AddAsync(waterEstimations);
                    await Context.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    Log.LogError(e, "Repository: failed to save WaterEstimation");
                }
            }
            return false;
        }

        public async Task<bool> AddWaterInfoAsync(int CityId, WaterInfo waterInfo)
        {
            var city = await Context.Cities.Where(x => x.CityId == CityId).FirstOrDefaultAsync();
            if (city != null)
            {
                waterInfo.City = city;
                try
                {
                    await Context.WaterInfos.AddAsync(waterInfo);
                    await Context.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    Log.LogError(e, "Repository: failed to save waterInfo");
                }
            }
            return false;
        }

        public async Task<List<City>> GetCitiesAsync()
        {
            return await Context.Cities
                .Include(y => y.waterEstimations)
                .Include(z => z.waterInfos)
                .ToListAsync();
        }

        public async Task<City> GetCityAsync(string name)
        {
            return await Context.Cities.Where(x => x.Name == name)
                .Include(y => y.waterEstimations)
                .Include(z => z.waterInfos)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetCityIdAsync(string name)
        {
            var city = await Context.Cities.Where(x => x.Name == name).FirstOrDefaultAsync();
            if (city != null)
                return city.CityId;
            else
            {
                Log.LogWarning($"Repository: City with {name} doesn't exists!");
                return -1;
            }

        }

        public async Task<bool> SaveCityAsync(City city)
        {
            try
            {
                await Context.AddAsync(city);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.LogError(e, "Repository: failed to save City");
                return false;
            }
        }
    }
}
