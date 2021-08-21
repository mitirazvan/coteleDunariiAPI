using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoteleDunarii.Data;
using CoteleDunarii.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.EntityFrameworkCore;

namespace CoteleDunarii.WebScrapper
{
    public class AdfjScrapper: IScrapper
    {
        private readonly int kLocalitatea = 0;
        private readonly int kKms = 1;
        private readonly int kNivelulApei = 2;
        private readonly int kVariatia = 3;
        private readonly int k24H = 4;
        private readonly int k48H = 5;
        private readonly int k72H = 6;
        private readonly int k96H = 7;
        private readonly int k120H = 8;
        private readonly int kTemperaturaMasurata = 9;
        private readonly int kDataActualizarii = 10;

        private readonly string _adfjUrl = "http://www.afdj.ro/ro/cotele-dunarii";
        private ILogger<AdfjScrapper> _logger;
        private CoteleDunariiContext _db;

        public AdfjScrapper(ILogger<AdfjScrapper> logger, CoteleDunariiContext dbContext)
        {
            _logger = logger;
            _db = dbContext;
        }

        public async Task<bool> RetrieveData()
        {
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(_adfjUrl);

            var div = doc.GetElementbyId("block-views-tabel-cotele-dunarii-block-1");

            var htmlTableHead = div.SelectSingleNode("//table/thead");
            var tableHeader = from row in htmlTableHead.SelectNodes("tr").Cast<HtmlNode>()
                              from cell in row.SelectNodes("th|td").Cast<HtmlNode>()
                              select new { Cell_Text = cell.InnerText.Trim() };

            var numberOfRecordsPerRow = tableHeader.Count();

            var htmlTableValues = div.SelectSingleNode("//table/tbody");
            var tableValues = (from row in htmlTableValues.SelectNodes("tr").Cast<HtmlNode>()
                              from cell in row.SelectNodes("th|td").Cast<HtmlNode>()
                              select new { Cell_Text = cell.InnerText.Trim() }).ToList();

            int index = 0;
            bool hasData = true;
            while(hasData)
            {
                try
                {
                    string cityName = tableValues[index + kLocalitatea].Cell_Text;
                    var cityFromDb = await _db.City.Where(x => x.Name == cityName).Include(y => y.waterEstimations).Include(z => z.waterInfo).FirstOrDefaultAsync();
                    if(cityFromDb != null)
                    {
                        cityFromDb.waterInfo.Elevation = tableValues[index + kNivelulApei].Cell_Text;
                        cityFromDb.waterInfo.Temperature = tableValues[index + kTemperaturaMasurata].Cell_Text;
                        cityFromDb.waterInfo.Variation = Convert.ToInt32(tableValues[index + kVariatia].Cell_Text);
                        cityFromDb.waterInfo.ReadTime = DateTime.ParseExact(tableValues[index + kDataActualizarii].Cell_Text, "dd/MM/yyyy", null);

                        cityFromDb.waterEstimations.ReadTime = DateTime.ParseExact(tableValues[index + kDataActualizarii].Cell_Text, "dd/MM/yyyy", null);
                        cityFromDb.waterEstimations.Next24h = tableValues[index + k24H].Cell_Text;
                        cityFromDb.waterEstimations.Next48h = tableValues[index + k48H].Cell_Text;
                        cityFromDb.waterEstimations.Next72h = tableValues[index + k72H].Cell_Text;
                        cityFromDb.waterEstimations.Next96h = tableValues[index + k96H].Cell_Text;
                        cityFromDb.waterEstimations.Next120h = tableValues[index + k120H].Cell_Text;

                        _db.City.Update(cityFromDb);
                    }
                    else
                    {
                        var city = new City
                        {
                            Name = cityName,
                            Km = tableValues[index + kKms].Cell_Text,
                            waterInfo = new WaterInfo
                            {
                                Elevation = tableValues[index + kNivelulApei].Cell_Text,
                                Temperature = tableValues[index + kTemperaturaMasurata].Cell_Text,
                                Variation = Convert.ToInt32(tableValues[index + kVariatia].Cell_Text),
                                ReadTime = DateTime.ParseExact(tableValues[index + kDataActualizarii].Cell_Text, "dd/MM/yyyy", null)
                            },
                            waterEstimations = new WaterEstimations
                            {
                                ReadTime = DateTime.ParseExact(tableValues[index + kDataActualizarii].Cell_Text, "dd/MM/yyyy", null),
                                Next24h = tableValues[index + k24H].Cell_Text,
                                Next48h = tableValues[index + k48H].Cell_Text,
                                Next72h = tableValues[index + k72H].Cell_Text,
                                Next96h = tableValues[index + k96H].Cell_Text,
                                Next120h = tableValues[index + k120H].Cell_Text,
                            }
                        };                       
                        _db.City.Add(city);
                    }
                    await _db.SaveChangesAsync();
                }
                catch
                {
                    _logger.LogError("Failed to decode response from ADFJ");
                }

                index += numberOfRecordsPerRow;
                hasData = tableValues.Count() > index;
            }
            


            return true;
        }
    }
}
