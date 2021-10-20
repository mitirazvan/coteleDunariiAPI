using CoteleDunarii.Services.Dtos;
using CoteleDunarii.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoteleDunarii.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoteleDunariiController : ControllerBase
    {
        private readonly ICityService _service;

        public CoteleDunariiController(ICityService service)
        {
            _service = service;
        }

        // GET: api/CoteleDunarii
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities(DateTime? minDate = null)
        {
            return await _service.GetCities(minDate);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<CityDto>> GetCity(string name, DateTime? minDate = null)
        {
            return await _service.GetCity(name, minDate);
        }
    }
}
