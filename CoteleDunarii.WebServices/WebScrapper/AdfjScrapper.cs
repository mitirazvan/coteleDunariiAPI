using CoteleDunarii.Services.Dtos;
using CoteleDunarii.Services.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoteleDunarii.WebServices.WebScrapper
{
    public class AdfjScrapper : IScrapper
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
        private ICityService _service;

        public AdfjScrapper(ILogger<AdfjScrapper> logger, ICityService service)
        {
            _logger = logger;
            _service = service;
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
            while (hasData)
            {
                try
                {
                    string cityName = tableValues[index + kLocalitatea].Cell_Text;

                    var city = new CityDto
                    {
                        Name = cityName,
                        Km = tableValues[index + kKms].Cell_Text,
                        WaterInfos = new List<WaterInfoDto>{new WaterInfoDto
                        {
                            Elevation = tableValues[index + kNivelulApei].Cell_Text,
                            Temperature = tableValues[index + kTemperaturaMasurata].Cell_Text,
                            Variation = Convert.ToInt32(tableValues[index + kVariatia].Cell_Text),
                            ReadTime = DateTime.ParseExact(tableValues[index + kDataActualizarii].Cell_Text, "dd/MM/yyyy", null)
                        }},
                        WaterEstimations = new List<WaterEstimationDto>{ new WaterEstimationDto
                        {
                            ReadTime = DateTime.ParseExact(tableValues[index + kDataActualizarii].Cell_Text, "dd/MM/yyyy", null),
                            Next24h = tableValues[index + k24H].Cell_Text,
                            Next48h = tableValues[index + k48H].Cell_Text,
                            Next72h = tableValues[index + k72H].Cell_Text,
                            Next96h = tableValues[index + k96H].Cell_Text,
                            Next120h = tableValues[index + k120H].Cell_Text,
                        }}
                    };
                    await _service.SaveCity(city);

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
