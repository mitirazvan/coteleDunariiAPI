using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoteleDunarii.Models;

namespace CoteleDunarii.Data
{
    public class CoteleDunariiContext : DbContext
    {
        public CoteleDunariiContext (DbContextOptions<CoteleDunariiContext> options)
            : base(options)
        {
        }

        public DbSet<City> City { get; set; }        
    }
}
