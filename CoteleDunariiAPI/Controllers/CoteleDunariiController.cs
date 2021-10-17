using CoteleDunarii.Services.DTOs;
using CoteleDunarii.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCity()
        {
            return await _service.GetCities();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<CityDto>> GetCity(string name)
        {
            return await _service.GetCity(name);
        }
    }
}
