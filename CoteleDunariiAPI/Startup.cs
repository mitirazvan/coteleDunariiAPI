using CoteleDunarii.Data;
using CoteleDunarii.Repository;
using CoteleDunarii.Repository.Interfaces;
using CoteleDunarii.Services;
using CoteleDunarii.Services.Interfaces;
using CoteleDunarii.WebServices.WebScrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoteleDunarii
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<CoteleDunariiContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            services.AddAutoMapper(typeof(Startup));

            //            Mapper.AssertConfigurationIsValid();

            services.AddScoped<IScrapper, AdfjScrapper>();

            // Register Repositories
            services.AddScoped<ICityRepostirory, CityRepository>();

            // Register Services
            services.AddScoped<ICityService, CityService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
