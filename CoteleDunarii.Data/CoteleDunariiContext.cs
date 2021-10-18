using CoteleDunarii.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoteleDunarii.Data
{
    public class CoteleDunariiContext : DbContext
    {
        public CoteleDunariiContext(DbContextOptions<CoteleDunariiContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<WaterEstimations> WaterEstimations { get; set; }
        public DbSet<WaterInfo> WaterInfos { get; set; }
    }
}
