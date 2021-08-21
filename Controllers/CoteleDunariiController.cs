using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoteleDunarii.Data;
using CoteleDunarii.Models;

namespace CoteleDunarii.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoteleDunariiController : ControllerBase
    {
        private readonly CoteleDunariiContext _context;

        public CoteleDunariiController(CoteleDunariiContext context)
        {
            _context = context;
        }

        // GET: api/CoteleDunarii
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCity()
        {
            return await _context.City
                .Include(y => y.waterEstimations)
                .Include(z => z.waterInfo)
                .ToListAsync();
        }

        // GET: api/CoteleDunarii/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {            
            var city = await _context.City.Where(x => x.CityId == id).Include(y => y.waterEstimations).Include(z => z.waterInfo).FirstOrDefaultAsync();

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }
    }
}
